<?php

// Example: http://192.168.1.9/abf-browser/api/v4/project/?path=X:\Projects\Aging-eCB
require_once("../shared.php");
require_once("../paths.php");
require_once("../json-experiment.php");

if (!isset($_GET["path"]))
    errorAndDie(400, "request error", "'path' is required");

$localFolderPath = LocalPathFromX($_GET["path"]);
if (!is_dir($localFolderPath))
    errorAndDie(500, "path error", "folder not found: $localFolderPath");

$localProjectFilePath = $localFolderPath . DIRECTORY_SEPARATOR . "project.json";
if (!is_file($localProjectFilePath))
    errorAndDie(500, "path error", "file not found: $localProjectFilePath");

$project = json_decode(file_get_contents($localProjectFilePath));
$project->abfExperiments = readExperiments($localFolderPath . DIRECTORY_SEPARATOR . "abfs");

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($project, JSON_PRETTY_PRINT);
