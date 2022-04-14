<?php

// ABFFOLDER.PHP
// return a keyed array containing every cell in the ABF day folder
function getCellsAbfday(string $folderPath)
{
    $cells = [];
    $cells["cell1"] = "lolz";
    $cells["cell2"] = "lolz";
    $cells["cell3"] = "lolz";
    return $cells;
}

// TODO: optimize using two pointer variables
function GetAbfsByParent(array $filenames, array $analysisFilenames)
{
    $parent = "orphan";
    $abfs[$parent] = [];
    $analyses[$parent] = [];

    foreach ($filenames as $filename) {
        if (!EndsWith($filename, ".abf"))
            continue;
        $supportFiles = GetSupportFiles($filenames, $filename);
        if (count($supportFiles) > 0) {
            $parent = GetAbfID($filename);
            $abfs[$parent] = [];
            $analyses[$parent] = [];
        }

        array_push($abfs[$parent], $filename);
        $abfID = GetAbfID($filename);
        foreach ($analysisFilenames as $analysisFilename) {
            if (StartsWith($analysisFilename, $abfID))
                array_push($analyses[$parent], $analysisFilename);
        }
    }

    if (count($abfs["orphan"]) == 0) {
        unset($abfs["orphan"]);
    }

    return [$abfs, $analyses];
}


/* return all the non-ABF files with the same base filename */
function GetSupportFiles(array $filenames, string $abfFilename)
{
    $abfID = GetAbfID($abfFilename);
    $matchingFiles = [];
    foreach ($filenames as $filename) {
        if (EndsWith($filename, ".abf"))
            continue;
        if (StartsWith($filename, $abfID))
            $matchingFiles[] = $filename;
    }
    return $matchingFiles;
}

/* return the abfID (basename) given an ABF filename */
function GetAbfID(string $filename): string
{
    if (!EndsWith($filename, ".abf"))
        throw new InvalidArgumentException("not an ABF filename: $filename");

    return substr($filename, 0, strlen($filename) - 4);
}

//////////////////////////////////////////////////////////////////////////////////////////////////

// Example: http://192.168.1.9/abf-browser/api/v4/experiment/?path=X:\Projects\Aging-eCB\abfs\exp1%20-%20DSI%20in%20CA1
require_once("../shared.php");
require_once("../paths.php");
require_once("../tools.php");
require_once("../json-experiment.php");

if (!isset($_GET["path"]))
    errorAndDie(400, "request error", "'path' is required");

$localFolderPath = LocalPathFromX($_GET["path"]);
if (!is_dir($localFolderPath))
    errorAndDie(500, "path error", "folder not found: $localFolderPath");

$localExperimentFilePath = $localFolderPath . DIRECTORY_SEPARATOR . "experiment.json";
if (!is_file($localExperimentFilePath))
    errorAndDie(500, "path error", "file not found: $localExperimentFilePath");

// start return object from the experiment JSON file
$experiment = json_decode(file_get_contents($localExperimentFilePath));

// add files, folders, and abfdayFolders
foreach (scandir($localFolderPath) as $fname) {
    if (StartsWith($fname, "."))
        continue;
    $localPath = $localFolderPath . DIRECTORY_SEPARATOR . $fname;
    if (is_dir($localPath)) {
        $experiment->folders[] = $fname;
        $abfdayFilePath = $localPath . DIRECTORY_SEPARATOR . "abfday.json";
        if (is_file($abfdayFilePath)) {
            $experiment->abfdayFolders[] = $fname;
        }
    } else {
        $experiment->files[] = $fname;
    }
}

// add cell details from abfday JSON files
$experiment->cells = [];
foreach ($experiment->abfdayFolders as $abfdayFolderName) {

    $abfDayFolderPath = $localFolderPath . DIRECTORY_SEPARATOR . $abfdayFolderName;
    $abfDayFilenames = scandir($abfDayFolderPath);

    $abfDayAnalysisFolderPath = $abfDayFolderPath . DIRECTORY_SEPARATOR . "_autoanalysis";
    $abfDayAnalysisFilenames = [];
    if (is_dir($abfDayAnalysisFolderPath))
        $abfDayAnalysisFilenames = scandir($abfDayAnalysisFolderPath);

    [$abfsByParent, $analysesByParent] = GetAbfsByParent($abfDayFilenames, $abfDayAnalysisFilenames);

    $parents = array_keys($abfsByParent);
    foreach ($parents as $parent) {
        $cell = array(
            "folder" => $abfdayFolderName,
            "children" => $abfsByParent[$parent],
            "analyses" => $analysesByParent[$parent],
        );
        $experiment->cells[] = $cell;
    }
}

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($experiment, JSON_PRETTY_PRINT);
