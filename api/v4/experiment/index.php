<?php

$timeStart = microtime(true);

require_once("../../tools/error.php");
require_once("../../tools/path.php");
require_once("../../tools/string.php");

if (!isset($_GET["path"]))
    errorAndDie(400, "request error", "'path' is required");

$localFolderPath = LocalPathFromX($_GET["path"]);
if (!is_dir($localFolderPath))
    errorAndDie(500, "path error", "folder not found: $localFolderPath");

$localExperimentFilePath = $localFolderPath . DIRECTORY_SEPARATOR . "experiment.json";
if (!is_file($localExperimentFilePath))
    errorAndDie(500, "path error", "file not found: $localExperimentFilePath");

// local and network file paths
$experiment = (object)[];
$experiment->path = LocalPathToX($localFolderPath);

// default to values read from JSON file
$experimentFromFile = json_decode(file_get_contents($localExperimentFilePath));
$experiment->title = $experimentFromFile->title;
$experiment->description = $experimentFromFile->description;
$experiment->notes = $experimentFromFile->notes;

// add raw filesystem details
$experiment->files = [];
$experiment->folders = [];
$experiment->abfdayFolders = [];
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

// DO NOT NOT look-up cells information at this level of abstract.
// The client must make those calls individually at the folder level.

$timeEnd = microtime(true);
$experiment->elapsedMilliseconds = ($timeEnd - $timeStart) * 1000;

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($experiment, JSON_PRETTY_PRINT);
