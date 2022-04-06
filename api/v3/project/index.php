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

$abfFolders = [
    "X:/Data/SD/DSI/CA1/Coronal",
    "X:/Data/SD/practice/Jordan",
    "X:/Data/SD/practice/Scott/2022/2022-01-04-AON",
    "X:/Data/DIC2/2013/05-2013/*",
];

$response = array(
    "title" => "Important Project",
    "subtitle" => "Looking into the things that matter",
    "notes" => "blah blah blah",
    "path" => LocalPathToX($pathX),
    "abfFolders" => $abfFolders,
    "abfFoldersScanned" => getValidFolderPaths($abfFolders),
);

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($response, JSON_PRETTY_PRINT);
