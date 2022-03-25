<?php

error_reporting(-1);
class BinaryFileReader
{
    public $fileSize;
    public $fileSizeMB;
    private $fb;

    public function Open()
    {
        $this->fileSize = filesize($this->path);
        $this->fileSizeMB = round($this->fileSize / 1024 / 1024, 2);
        $this->fb = fopen($this->path, "rb");
    }

    public function Close()
    {
        fclose($this->fb);
    }

    public function ReadString($letterCount, $firstByte = -1)
    {
        if ($firstByte >= 0)
            fseek($this->fb, $firstByte);
        return fread($this->fb, $letterCount);
    }

    public function ReadUInt16($firstByte = -1)
    {
        if ($firstByte >= 0)
            fseek($this->fb, $firstByte);
        return unpack("h", fread($this->fb, 2))[1];
    }

    public function ReadUInt32($firstByte = -1)
    {
        if ($firstByte >= 0)
            fseek($this->fb, $firstByte);
        return unpack("I", fread($this->fb, 4))[1];
    }

    public function ReadFloat($firstByte = -1)
    {
        if ($firstByte >= 0)
            fseek($this->fb, $firstByte);
        return unpack("f", fread($this->fb, 4))[1];
    }
}
