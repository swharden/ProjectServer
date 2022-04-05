<?php

/*  This API gives information about cells from a single folder:
    List of parents abfIDs each with child count, color, comment, and group.
    
    http://192.168.1.9/abf-browser/api/v3/cells-folder/?folder=X:/Data/SD/practice/Scott/2022/2022-01-04-AON
    http://192.168.1.9/abf-browser/api/v3/cells-folder/?folder=X:/Data/2P01/2008/03-2008/3-25-08-BN
  
 */

$start_time = microtime(true);

require_once("../../shared.php");
require_once("../../phpABF/ABF.php");

if (!isset($_GET["folder"]))
    errorAndDie(400, "URL Error", "'folder' is required");

$abfFolderPathX = str_replace('\\', '/', $_GET["folder"]);
$abfFolderPath = LocalPathFromX($abfFolderPathX);
if (!is_dir($abfFolderPath))
    errorAndDie(500, "Server Error", "folder does not exist: $abfFolderPath");

$abfFilePaths = array_slice(scandir($abfFolderPath), 2);
for ($i=0; $i<count($abfFilePaths); $i++){
    $abfFilePaths[$i] = $abfFolderPath . DIRECTORY_SEPARATOR . $abfFilePaths[$i];
}

$analysisFolderPath = $abfFolderPath . DIRECTORY_SEPARATOR . "_autoanalysis";
$analysisFilePaths = is_dir($analysisFolderPath) ? array_slice(scandir($analysisFolderPath), 2) : [];
for ($i=0; $i<count($analysisFilePaths); $i++){
    $analysisFilePaths[$i] = $analysisFolderPath . DIRECTORY_SEPARATOR . $analysisFilePaths[$i];
}

$experimentTextFilePath = $abfFolderPath . DIRECTORY_SEPARATOR . "experiment.txt";
$experimentFileContents = is_file($experimentTextFilePath) ? file_get_contents($experimentTextFilePath) : null;

$cellsTextFilePath = $abfFolderPath . DIRECTORY_SEPARATOR . "cells.txt";
$cellsFileContents = is_file($cellsTextFilePath) ? file_get_contents($cellsTextFilePath) : "";

$parentInfos = GetParentInfos($abfFilePaths, $analysisFilePaths, $cellsFileContents);

$end_time = microtime(true);

$response = array(
    "timeStarted" => $start_time,
    "timeFinished" => $end_time,
    "elapsedMilliseconds" => ($end_time - $start_time) * 1000,
    "folderPathLocal" => $abfFolderPath,
    "folderPathNetwork" => $abfFolderPathX,
    "cells" => $parentInfos,
);

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($response, JSON_PRETTY_PRINT);
