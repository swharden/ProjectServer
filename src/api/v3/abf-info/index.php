<?php
require_once("../../shared.php");
require_once("../../phpABF/ABF.php");

if ($_SERVER["REQUEST_METHOD"] != "POST")
    errorAndDie(400, "bad request type", "POST expected");

$postString = file_get_contents('php://input');
if (strpos($postString, '\\'))
    errorAndDie(400, "JSON Error", "do not use backslashes (only forward slashes)");

$post = json_decode($postString, true);

if (json_last_error() != JSON_ERROR_NONE)
    errorAndDie(400, "JSON Error", "json_last_error() returned " . json_last_error());

if (!array_key_exists("abf-path", $post))
    errorAndDie(400, "JSON Error", "abf-path is required");

$abfPath = $post["abf-path"];
$abfPath = LocalPathFromX($abfPath);
if (!file_exists($abfPath))
    errorAndDie(500, "Server Error", "ABF file not found");

$abf = new ABF($abfPath);
echo json_encode($abf);
