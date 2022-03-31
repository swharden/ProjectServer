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

echo json_encode(GetMenuItems($abfFolderPath), JSON_PRETTY_PRINT);
