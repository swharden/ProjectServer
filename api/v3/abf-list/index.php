<?php
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

$obj = (object)[
    "abf-folder-path" => LocalPathToX(realpath($abfFolderPath)),
    "filenames" => $fileNames,
    "abfs-by-parent" => GetAbfsByParent($fileNames),
];

echo json_encode($obj);
