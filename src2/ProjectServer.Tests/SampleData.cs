namespace ProjectServer.Tests;

internal static class SampleData
{
    public static string Folder => Path.GetFullPath("../../../../../dev/sample-data");

    [Test]
    public static void Test_DataFolder_Exists()
    {
        Console.WriteLine(Folder);
        Assert.That(Directory.Exists(Folder));
    }
}
