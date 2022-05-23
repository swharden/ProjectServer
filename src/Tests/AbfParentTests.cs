namespace ProjectServer.Tests;

internal class AbfParentTests
{
    [Ignore("depends on local drive")]
    [Test]
    public void Test_AbfFolder_Scan()
    {
        string testFolder = @"X:\Data\SD\DSI\PFC\abfs";
        Shared.AbfFolder f = Shared.AbfFolder.Scan(testFolder);
        Console.WriteLine(f);
        foreach (var p in f.AbfParents)
            Console.WriteLine(p);
    }

    [Ignore("depends on local drive")]
    [Test]
    public void Test_AbfFolder_NoCellsFile()
    {
        string abfFolderPath = @"X:/Data/SD/DSI/CA1/Coronal";
        Shared.AbfFolder f = Shared.AbfFolder.Scan(abfFolderPath);
        Console.WriteLine(f);
        foreach (var p in f.AbfParents)
            Console.WriteLine(p);
    }
}
