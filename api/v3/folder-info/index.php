<?php
header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");

require_once("../../shared.php");
require_once("../../phpABF/ABF.php");

if (!isset($_GET["folder"]))
    errorAndDie(400, "URL Error", "'folder' is required");

$abfFolderPath = str_replace('\\', '/', $_GET["folder"]);
$abfFolderPath = LocalPathFromX($abfFolderPath);
if (!is_dir($abfFolderPath))
    errorAndDie(500, "Server Error", "folder does not exist: $abfFolderPath");

$fileNames = array_slice(scandir($abfFolderPath), 2);

$analysisFolderPath = $abfFolderPath . DIRECTORY_SEPARATOR . "_autoanalysis";
$analysisFileNames = is_dir($analysisFolderPath) ? array_slice(scandir($analysisFolderPath), 2) : [];

$experimentTextFilePath = $abfFolderPath . DIRECTORY_SEPARATOR . "experiment.txt";
$experimentFileContents = is_file($experimentTextFilePath) ? file_get_contents($experimentTextFilePath) : null;

$cellsTextFilePath = $abfFolderPath . DIRECTORY_SEPARATOR . "cells.txt";
$cellsFileContents = is_file($cellsTextFilePath) ? file_get_contents($cellsTextFilePath) : null;

$obj = (object)[
    "folder" => LocalPathToX(realpath($abfFolderPath)),
    "filenames" => $fileNames,
    "analysis-filenames" => $analysisFileNames,
    "experiment.txt" => $experimentFileContents,
    "cells.txt" => $cellsFileContents,
];

echo json_encode($obj, JSON_PRETTY_PRINT);
