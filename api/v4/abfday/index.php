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

// create response object with default values
$abfDay = (object)[];
$abfDay->path = LocalPathToX($localFolderPath);
$abfDay->operator = null;
$abfDay->animal = null;
$abfDay->bath = null;
$abfDay->internal = null;
$abfDay->drugs = null;
$abfDay->notes = null;
$abfDay->cells = null;

// replace default values with those from the JSON file
$localAbfdayFilePath = $localFolderPath . DIRECTORY_SEPARATOR . "abfday.json";
if (is_file($localAbfdayFilePath)) {
    $abfDayFile = json_decode(file_get_contents($localAbfdayFilePath));
    $abfDay->operator = $abfDayFile->operator;
    $abfDay->animal = $abfDayFile->animal;
    $abfDay->bath = $abfDayFile->bath;
    $abfDay->internal = $abfDayFile->internal;
    $abfDay->drugs = $abfDayFile->drugs;
    $abfDay->notes = $abfDayFile->notes;
    $abfDay->cells = $abfDayFile->cells;
}

// add raw filesystem details
$abfDay->files = [];
$abfDay->folders = [];
foreach (scandir($localFolderPath) as $fname) {
    if (StartsWith($fname, ".") || StartsWith($fname, "_"))
        continue;
    $localPath = $localFolderPath . DIRECTORY_SEPARATOR . $fname;
    if (is_dir($localPath)) {
        $abfDay->folders[] = $fname;
    } else {
        $abfDay->files[] = $fname;
    }
}

// add analysis files
$abfDay->analyses = [];
$localAnalysisPath = $localFolderPath . DIRECTORY_SEPARATOR . "_autoanalysis";
if (is_dir($localAnalysisPath)) {
    foreach (scandir($localAnalysisPath) as $fname) {
        if (StartsWith($fname, ".") || StartsWith($fname, "_"))
            continue;
        $abfDay->analyses[] = $fname;
    }
}

$timeEnd = microtime(true);
$abfDay->elapsedMilliseconds = ($timeEnd - $timeStart) * 1000;

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($abfDay, JSON_PRETTY_PRINT);
