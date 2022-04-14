<?php

require_once("../../tools/error.php");
require_once("../../tools/path.php");
require_once("../../tools/string.php");

if (!isset($_GET["path"]))
    errorAndDie(400, "request error", "'path' is required");

$localFolderPath = LocalPathFromX($_GET["path"]);
if (!is_dir($localFolderPath))
    errorAndDie(500, "path error", "folder not found: $localFolderPath");

$localProjectFilePath = $localFolderPath . DIRECTORY_SEPARATOR . "project.json";
if (!is_file($localProjectFilePath))
    errorAndDie(500, "path error", "file not found: $localProjectFilePath");

$project = json_decode(file_get_contents($localProjectFilePath));

// add raw filesystem details
$project->files = [];
$project->folders = [];
foreach (scandir($localFolderPath) as $fname) {
    if (StartsWith($fname, "."))
        continue;
    $localPath = $localFolderPath . DIRECTORY_SEPARATOR . $fname;
    if (is_dir($localPath)) {
        $project->folders[] = $fname;
    } else {
        $project->files[] = $fname;
    }
}

// add experiment folders
$localExperimentFolder =  $localFolderPath . DIRECTORY_SEPARATOR . "experiments";
$project->experimentFolders = [];
if (is_dir($localExperimentFolder)) {
    foreach (scandir($localExperimentFolder) as $fname) {
        $experimentJsonFilePath = $localExperimentFolder . DIRECTORY_SEPARATOR . $fname . DIRECTORY_SEPARATOR . "experiment.json";
        if (is_file($experimentJsonFilePath)) {
            $project->experimentFolders[] = $fname;
        }
    }
}

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($project, JSON_PRETTY_PRINT);
