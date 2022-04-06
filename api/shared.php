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
    header('Content-Type: application/json');
    header("Access-Control-Allow-Origin: *");
    echo json_encode($responseItems, JSON_PRETTY_PRINT);
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

function LocalPathToUrl(string $path): string
{
    $path = LocalPathToX($path);
    $path = str_replace('x:/', 'http://192.168.1.9/X/', $path);
    $path = str_replace('X:/', 'http://192.168.1.9/X/', $path);
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

function GetParentInfos(array $abfFilePaths, array $analysisFilePaths, string $cellsTxt)
{
    $abfFolderPath = dirname($abfFilePaths[0]);

    // this is the object we will build and return
    $cellInfos = [];

    // pre-process analysis file list to isolate images
    $analysisImagePaths = [];
    foreach ($analysisFilePaths as $analysisFilePath) {
        if (EndsWith($analysisFilePath, ".png") || EndsWith($analysisFilePath, ".jpg"))
            $analysisImagePaths[] = $analysisFilePath;
    }

    // initiate all parents using the ABF list
    $abfsByParent = GetAbfsByParent($abfFilePaths);
    foreach (array_keys($abfsByParent) as $parentID) {

        $abfAnalysisImages = [];
        foreach ($abfsByParent[$parentID] as $abfPath) {
            $abfFilename = basename($abfPath);
            $abfID = substr($abfFilename, 0, strlen($abfFilename) - 4);
            foreach ($analysisImagePaths as $analysisImagePath) {
                $analysisImageBasename = basename($analysisImagePath);
                if (StartsWith($analysisImageBasename, $abfID)) {
                    $abfAnalysisImages[] = LocalPathToUrl($analysisImagePath);
                }
            }
        }

        $childAbfPaths = [];
        foreach ($abfsByParent[$parentID] as $abfPath)
            $childAbfPaths[] = LocalPathToX($abfFolderPath . DIRECTORY_SEPARATOR . $abfPath);

        $cellInfos[$parentID] = array(
            "parentID" => $parentID,
            "color" => null,
            "comment" => null,
            "group" => null,
            "abfPaths" => $childAbfPaths,
            "analysisImages" => $abfAnalysisImages,
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
function GetAbfsByParent(array $abfFilePaths)
{
    $filenames = [];
    foreach ($abfFilePaths as $abfFilePath)
        $filenames[] = basename($abfFilePath);

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

/* Given a list of folder paths (with optional trailing wildcards),
   return a list of all valid existing paths 
*/
function getValidFolderPaths(array $abfFolders, string $wildcard = '*'): array
{
    $validFolderPaths = [];
    foreach ($abfFolders as $abfFolder) {
        $localFolder = LocalPathFromX($abfFolder);
        if (basename($localFolder) == $wildcard) {
            $localFolder = dirname($localFolder);
            if (is_dir($localFolder)) {
                $subFolderNames = array_slice(scandir($localFolder), 2);
                foreach ($subFolderNames as $subFolderName) {
                    $subFolderPath = $localFolder . DIRECTORY_SEPARATOR . $subFolderName;
                    if (is_dir($subFolderPath))
                        $validFolderPaths[] = LocalPathToX($subFolderPath);
                }
            }
        } else {
            if (is_dir($localFolder)) {
                $validFolderPaths[] = LocalPathToX($localFolder);
            }
        }
    }
    return $validFolderPaths;
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
