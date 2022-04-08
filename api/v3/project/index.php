<?php

/*  This API interacts with ABFPROJ files that store multi-folder ABF project details
    
    http://192.168.1.9/abf-browser/api/v3/project/?path=X:\Users_Public\Scott\temp\example.abfproj

 */

$start_time = microtime(true);

require_once("../../shared.php");
require_once("../../phpABF/ABF.php");

if (!isset($_GET["path"]))
    errorAndDie(400, "URL Error", "'path' is required");

$pathX = str_replace('\\', '/', $_GET["path"]);
$pathLocal = LocalPathFromX($pathX);
if (!is_file($pathLocal))
    errorAndDie(500, "Server Error", "file does not exist: $pathLocal");

$end_time = microtime(true);

// load json from file
$loadedProject = json_decode(file_get_contents($pathLocal));

$response = array(
    "timeStarted" => $start_time,
    "timeFinished" => $end_time,
    "elapsedMilliseconds" => ($end_time - $start_time) * 1000,
    "title" => $loadedProject->title,
    "subtitle" => $loadedProject->subtitle,
    "notes" => $loadedProject->notes,
    "path" => LocalPathToX($pathX),
    "abfFolders" => $loadedProject->abfFolders,
    "abfFoldersScanned" => getValidFolderPaths($loadedProject->abfFolders),
);

// write contents to file
//file_put_contents($pathLocal, json_encode($response, JSON_PRETTY_PRINT));

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($response, JSON_PRETTY_PRINT);
