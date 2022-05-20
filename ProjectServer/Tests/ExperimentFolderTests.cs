using ProjectServer.Shared;

namespace ProjectServer.Tests;

internal class ExperimentFolderTests
{
    [Test]
    public void Test_Experiment_Load()
    {
        ExperimentFolder exp = ExperimentFolder.Scan(@"X:\Data\zProjects\Aging and eCBs\experiments\CA1\DSI");
        Console.WriteLine(exp);

        exp.Info.SaveJsonFile("test123.json");
        ExperimentFolderInfo info2 = ExperimentFolderInfo.LoadJsonFile("test123.json");
        Console.WriteLine(info2);
    }
}
