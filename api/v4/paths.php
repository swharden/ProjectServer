<?php

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
