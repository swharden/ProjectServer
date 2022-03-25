<?php
require_once("../../shared.php");
require_once("../../phpABF/ABF.php");

$postString = file_get_contents('php://input');
if (strpos($postString, '\\'))
    errorAndDie(400, "JSON Error", "do not use backslashes (only forward slashes)");

$post = json_decode($postString, true);

if (json_last_error() != JSON_ERROR_NONE)
    errorAndDie(400, "JSON Error", "json_last_error() returned " . json_last_error());

if (!array_key_exists("abf-folder-path", $post))
    errorAndDie(400, "JSON Error", "abf-folder-path is required");

$abfFolderPath = $post["abf-folder-path"];
$abfFolderPath = LocalPathFromX($abfFolderPath);
if (!is_dir($abfFolderPath))
    errorAndDie(500, "Server Error", "folder does not exist");

$fileNames = scandir($abfFolderPath);

$obj = (object)[
    "abf-folder-path" => LocalPathToX(realpath($abfFolderPath)),
    "abfs-by-parent" => GetAbfsByParent($fileNames),
];

echo json_encode($obj);
