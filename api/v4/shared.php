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
    header('Content-Type: application/json');
    header("Access-Control-Allow-Origin: *");
    echo json_encode($responseItems, JSON_PRETTY_PRINT);
    die();
}
