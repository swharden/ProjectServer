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

function GetParentInfos(array $filenames, string $cellsTxt)
{
    $cellInfos = [];

    // initiate all parents using the ABF list
    $abfsByParent = GetAbfsByParent($filenames);
    foreach (array_keys($abfsByParent) as $parentID) {
        $cellInfos[$parentID] = array(
            "child-count" => count($abfsByParent[$parentID]),
            "color" => null,
            "comment" => null,
            "group" => null,
        );
    }

    // populate details for parents found in the cells file
    $currentGroup = null;
    foreach (explode("\n", $cellsTxt) as $rawLine) {
        $line = trim($rawLine);

        if (StartsWith($line, "---")) {
            $currentGroup = substr($line, 4);
            continue;
        }

        $line = explode(" ", $line, 3);
        $lineAbfID = $line[0];

        // skip parents defined in the text file not in the actual file list
        if (!array_key_exists($lineAbfID, $cellInfos))
            continue;

        $cellInfos[$lineAbfID]["group"] =  $currentGroup;

        if (count($line) >= 2)
            $cellInfos[$lineAbfID]["color"] =  ColorCodeToHex($line[1]);

        if (count($line) >= 3)
            $cellInfos[$lineAbfID]["comment"] =  $line[2];
    }

    return $cellInfos;
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

function GetMenuItems(string $abfFolderPath)
{
    $parents = [];

    $fileNames = array_slice(scandir($abfFolderPath), 2);
    $abfsByParent = GetAbfsByParent($fileNames);
    foreach (array_keys($abfsByParent) as $parentID) {
        $parents[$parentID] = array(
            "childCount" => count($abfsByParent[$parentID]),
            "color" => "#00FF00",
            "comment" => "pyramidal",
        );
    }

    return $parents;
}

function ColorCodeToHex(string $code)
{
    if (StartsWith($code, "#"))
        return $code;

    $colorCodes = array(
        "" => "#FFFFFF",
        "?" => "#EEEEEE",
        "g" => "#00FF00",
        "g1" => "#00CC00",
        "g2" => "#009900",
        "b" => "#FF9999",
        "i" => "#CCCCCC",
        "s" => "#CCCCFF",
        "s1" => "#9999DD",
        "s2" => "#6666BB",
        "s3" => "#333399",
        "w" => "#FFFF00"
    );

    if (array_key_exists($code, $colorCodes)) {
        return $colorCodes[$code];
    } else {
        return null;
    }
}
