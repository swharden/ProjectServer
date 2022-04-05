<?php

/* This API gives information about a specific ABF 

    Success:
    http://192.168.1.9/abf-browser/api/v3/abf-info/?path=X:\Data\SD\practice\Scott\2022\2022-01-04-AON\2022_01_04_0013.abf

    Fail:
    http://192.168.1.9/abf-browser/api/v3/abf-info/
    http://192.168.1.9/abf-browser/api/v3/abf-info/?path=X:\Data\does\not\exist.abf
    http://192.168.1.9/abf-browser/api/v3/abf-info/?path=X:\Data\SD\practice\Scott\2022\2022-01-04-AON\2022_01_04_0020.tif
*/

require_once("../../shared.php");
require_once("../../phpABF/ABF.php");

if (!isset($_GET["path"]))
    errorAndDie(400, "URL Error", "'path' is required");

$localPath = LocalPathFromX($_GET["path"]);

if (!file_exists($localPath)) {
    errorAndDie(500, "ABF Error", "file not found");
}

try {
    $abf = new ABF($localPath);
    $abf->path = LocalPathToX($abf->path);
} catch (Exception $ex) {
    errorAndDie(500, "ABF Error", $ex->getMessage());
}

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($abf, JSON_PRETTY_PRINT);
