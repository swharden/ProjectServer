<?php

function readExperiment(string $localExperimentJsonPath)
{
    $experiment = json_decode(file_get_contents($localExperimentJsonPath));
    $experiment->path = LocalPathToX(dirname($localExperimentJsonPath));
    return $experiment;
}

function readExperiments(string $localFolderOfExperiments)
{
    if (!is_dir($localFolderOfExperiments))
        return [];

    $experiments = [];
    foreach (scandir($localFolderOfExperiments) as $subFolderName) {
        $experimentFolder = $localFolderOfExperiments . DIRECTORY_SEPARATOR . $subFolderName;
        $experimentsFile = $experimentFolder . DIRECTORY_SEPARATOR . "experiment.json";
        if (is_file($experimentsFile))
            $experiments[$subFolderName] = readExperiment($experimentsFile);
    }
    return $experiments;
}
