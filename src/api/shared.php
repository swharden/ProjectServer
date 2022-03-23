<?php

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
