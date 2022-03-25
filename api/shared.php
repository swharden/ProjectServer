<?php

use function PHPSTORM_META\type;

ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

function errorAndDie(int $code, string $error, string $message): void
{
    $responseItems = array(
        'timestamp' => gmdate('c'),
        'status' => $code,
        'error' => $error,
        'message' => $message,
    );

    http_response_code($code);
    echo json_encode($responseItems);
    die();
}

function LocalPathFromX(string $path): string
{
    $path = str_replace('x:', 'D:/X_Drive', $path);
    $path = str_replace('X:', 'D:/X_Drive', $path);
    return $path;
}

function LocalPathToX(string $path): string
{
    $path = str_replace('D:/X_Drive', 'X:', $path);
    $path = str_replace('D:\\X_Drive', 'X:', $path);
    $path = str_replace('\\', '/', $path);
    return $path;
}

function StartsWith($haystack, $needle)
{
    $length = strlen($needle);
    return substr($haystack, 0, $length) === $needle;
}

function EndsWith(string $haystack, string $needle)
{
    $length = strlen($needle);
    if (!$length) {
        return true;
    }
    return substr($haystack, -$length) === $needle;
}

/* return the abfID (basename) given an ABF filename */
function GetAbfID(string $filename): string
{
    if (!EndsWith($filename, ".abf"))
        throw new InvalidArgumentException("not an ABF filename: $filename");

    return substr($filename, 0, strlen($filename) - 4);
}

/* return all the non-ABF files with the same base filename */
function GetSupportFiles(array $filenames, string $abfFilename)
{
    $abfID = GetAbfID($abfFilename);
    $matchingFiles = [];
    foreach ($filenames as $filename) {
        if (EndsWith($filename, ".abf"))
            continue;
        if (StartsWith($filename, $abfID))
            $matchingFiles[] = $filename;
    }
    return $matchingFiles;
}

/* given a file list return a keyed array with abfIDs and filenames */
function GetAbfsByParent(array $filenames)
{
    $parent = "orphan";
    $abfs[$parent] = [];

    foreach ($filenames as $filename) {
        if (!EndsWith($filename, ".abf"))
            continue;
        $supportFiles = GetSupportFiles($filenames, $filename);
        if (count($supportFiles) > 0) {
            $parent = GetAbfID($filename);
            $abfs[$parent] = [];
        }

        array_push($abfs[$parent], $filename);
    }

    return $abfs;
}
