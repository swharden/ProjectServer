<?php

// Example: http://192.168.1.9/abf-browser/api/v4/abfday.php?path=X:\Projects\Aging-eCB\abfs\exp1 - DSI in CA1\2022-03-10

require_once("../shared.php");
require_once("../paths.php");
require_once("../tools.php");

if (!isset($_GET["path"]))
    errorAndDie(400, "request error", "'path' is required");

$localFolderPath = LocalPathFromX($_GET["path"]);
if (!is_dir($localFolderPath))
    errorAndDie(500, "path error", "folder not found: $localFolderPath");

$localAbfdayFilePath = $localFolderPath . DIRECTORY_SEPARATOR . "abfday.json";
if (!is_file($localAbfdayFilePath))
    errorAndDie(500, "path error", "file not found: $localAbfdayFilePath");

$abfDay = json_decode(file_get_contents($localAbfdayFilePath));

// TODO: identify parents manually using matched filenames
$fileNames = array_slice(scandir($localFolderPath), 2);
$fileNames = array_unique($fileNames);
sort($fileNames);
$abfDay->fileNames = $fileNames;

// get sorted unique list of parents
$parentIDs = [];
foreach ($abfDay->cells as $cell) {
    $parentIDs[] = $cell->id;
}
$parentIDs = array_unique($parentIDs);
sort($parentIDs);
$abfDay->parentIDs = $parentIDs;

// TODO: use double sliding arrow to match ABFs to parents
$filenamesWithParent = []; // TODO: fancier data type

if (StartsWith($fileNames[0], $parentIDs[0])) {
    $parentName = $parentIDs[0];
    $parentIndex = 0;
} else {
    $parentName = "orphan";
    $parentIndex = -1;
}

for ($i = 0; $i < count($fileNames); $i++) {
    $filename = $fileNames[$i];
    if (StartsWith($filename, $parentName)) {
        $filenamesWithParent[] = "$parentName $filename";
    } else {
        
    }
}

$abfDay->filenamesWithParent = $filenamesWithParent;

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
echo json_encode($abfDay, JSON_PRETTY_PRINT);