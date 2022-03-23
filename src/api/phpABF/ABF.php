<?

require "BinaryFileReader.php";

class ABF extends BinaryFileReader
{
    public $path;
    public $abfID;
    public $protocol;

    public $sweepCount;
    public $sweepLengthSec;
    public $channelCount;

    public $tagTimeSec;
    public $tagComments;
    public $tagCount;
    public $tagStrings;

    function __construct($path)
    {
        if (file_exists($path) == false)
            throw new Exception("file not found: $path");
        $this->path = realpath($path);
        $this->abfID = pathinfo($path)['filename'];

        $this->Open();

        $firstFourBytes = $this->ReadString(4, 0);
        if ($firstFourBytes == "ABF ")
            $this->ReadHeaderABF1();
        else if ($firstFourBytes == "ABF2")
            $this->ReadHeaderABF2();
        else
            throw new Exception("file is not ABF format");

        $this->Close();
    }

    private function ReadHeaderABF1()
    {
        // Header Values
        $nOperationMode = $this->ReadUInt16(8);
        $this->gapFree = ($nOperationMode == 3);
        $this->channelCount = $this->ReadUInt16(120);
        $this->sweepCount = $this->ReadUInt32(16);
        if ($this->gapFree)
            $this->sweepCount = 1;
        $fADCSampleInterval = $this->ReadFloat(122);
        $this->dataRate = 1e6 / $fADCSampleInterval / $this->channelCount;
        $dataPointCount = $this->ReadUInt32(10);
        $sweepPointCount = $dataPointCount / $this->sweepCount / $this->channelCount;
        $this->sweepLengthSec = $sweepPointCount / $this->dataRate;
        $protocolPath = trim($this->ReadString(256, 4898));
        $this->protocol = str_replace(".pro", "", basename($protocolPath));

        // Tag Comments
        $lTagSectionPtr = $this->ReadUInt32(44);
        $this->tagCount = $this->ReadUInt32(48);
        $this->tagTimesSec = array();
        $this->tagComments = array();
        $this->tagStrings = array();
        for ($i = 0; $i < $this->tagCount; $i++) {
            $tagStartByte = $lTagSectionPtr * 512 + $i * 64;
            $tagTime = $this->ReadUInt32($tagStartByte);
            $tagTimeSec = $tagTime * $fADCSampleInterval / 1e6 / $this->channelCount;
            $tagTimeMin = round($tagTimeSec / 60, 2);
            $tagComment = trim($this->ReadString(56));
            $tagType = $this->ReadUInt32(16);

            array_push($this->tagTimesSec, $tagTimeSec);
            array_push($this->tagComments, $tagComment);
            array_push($this->tagStrings, "$tagComment @ $tagTimeMin min");
        }
        $this->tagStrings = implode(", ", $this->tagStrings);
    }

    private function ReadHeaderABF2()
    {
        // get section byte location information
        $protocolSection_firstByte = $this->ReadUInt32(76) * 512;
        $protocolSection_size = $this->ReadUInt32();
        $protocolSection_count = $this->ReadUInt32();
        $adcSection_firstByte = $this->ReadUInt32(92) * 512;
        $adcSection_size = $this->ReadUInt32();
        $adcSection_count = $this->ReadUInt32();
        $stringsSection_firstByte = $this->ReadUInt32(220) * 512;
        $stringsSection_size = $this->ReadUInt32();
        $stringsSection_count = $this->ReadUInt32();
        $dataSection_firstByte = $this->ReadUInt32(236) * 512;
        $dataSection_size = $this->ReadUInt32();
        $dataSection_count = $this->ReadUInt32();

        // read useful values from fixed offsets relative to certain sections
        $nOperationMode = $this->ReadUInt16($protocolSection_firstByte);
        $this->gapFree = ($nOperationMode == 3);
        $fADCSequenceInterval = $this->ReadFloat($protocolSection_firstByte + 2);
        $sampleRate = 1e6 / $fADCSequenceInterval;
        $this->sweepCount = $this->ReadUInt32(12);
        if ($this->gapFree)
            $this->sweepCount = 1;
        $this->channelCount = $adcSection_count;
        $this->sweepLengthSec = $dataSection_count / $this->sweepCount / $this->channelCount / $sampleRate;

        // read indexed strings
        $firstString = $this->ReadString($stringsSection_firstByte, $stringsSection_size);
        $textIndex = strripos($firstString, "\x00\x00");
        $firstString = substr($firstString, $textIndex);
        $firstString = str_replace("\xb5", "\x75", $firstString); // make mu u
        $strings = explode("\x00", $firstString);
        array_shift($strings);

        // get protocol from strings list
        $uProtocolPathIndex = $this->ReadUInt32(72);
        $protocolPath = $strings[$uProtocolPathIndex];
        $this->protocol = basename($protocolPath);

        // determine where tags live
        $tagSectionFirstByte = $this->ReadUInt32(252) * 512;
        $tagSectionSize = $this->ReadUInt32();
        $tagSectionCount = $this->ReadUInt32();

        // read comment tags
        $fSynchTimeUnit = $this->ReadFloat($protocolSection_firstByte + 14);
        $commentTagType = 1;
        $this->tagTimesSec = array();
        $this->tagComments = array();
        $this->tagCount = 0;
        $this->tagStrings = array();
        for ($i = 0; $i < $tagSectionCount; $i++) {
            $tagByte = $tagSectionFirstByte + $i * $tagSectionSize;
            $lTagTime = $this->ReadUInt32($tagByte);
            $sTagComment = $this->ReadString(56);
            $nTagType = $this->ReadUInt16();
            if ($nTagType == $commentTagType) {
                $tagTimeSec = $lTagTime * $fSynchTimeUnit / 1e6;
                $tagTimeMin = round($tagTimeSec / 60, 2);
                $tagComment = trim($sTagComment);
                array_push($this->tagTimesSec, $tagTimeSec);
                array_push($this->tagComments, $tagComment);
                array_push($this->tagStrings, "$tagComment @ $tagTimeMin min");
                $this->tagCount += 1;
            }
        }
        $this->tagStrings = implode(", ", $this->tagStrings);
    }

    public function __toString()
    {
        return "ABF: $this->path";
    }

    public function ShowInfo()
    {
        echo "<pre>";
        echo "phpABF ShowInfo()\n";
        echo "path: $this->path\n";
        echo "abfID: $this->abfID\n";
        echo "fileSize: $this->fileSize\n";
        echo "fileSizeMB: $this->fileSizeMB\n";
        echo "protocol: $this->protocol\n";
        echo "sweeps: $this->sweepCount\n";
        echo "channels: $this->channelCount\n";
        echo "tags ($this->tagCount): $this->tagStrings\n";
        echo "</pre>";
    }
}
