<?php

// Example: http://192.168.1.9/abf-browser/api/v4/experiment/?path=X:\Projects\Aging-eCB\abfs\exp1%20-%20DSI%20in%20CA1
require_once("../shared.php");
require_once("../paths.php");
require_once("../json-experiment.php");

if (!isset($_GET["path"]))
    errorAndDie(400, "request error", "'path' is required");

$localFolderPath = LocalPathFromX($_GET["path"]);
if (!is_dir($localFolderPath))
    errorAndDie(500, "path error", "folder not found: $localFolderPath");

$localExperimentFilePath = $localFolderPath . DIRECTORY_SEPARATOR . "experiment.json";
if (!is_file($localExperimentFilePath))
    errorAndDie(500, "path error", "file not found: $localExperimentFilePath");

$experiment = json_decode(file_get_contents($localExperimentFilePath));
//$project->abfDays = readExperiments($localFolderPath . DIRECTORY_SEPARATOR . "abfs");

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($experiment, JSON_PRETTY_PRINT);