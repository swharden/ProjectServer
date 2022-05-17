using ProjectServer.Shared;
using System.Text.Json;

namespace ProjectServer.Tests;

public class AbfMultiFolderTests
{
    [Test]
    public void Test_MultiFolder_FromPath()
    {
        var info = AbfMultiFolderInfo.FromPath(@"X:\Data\SD\practice\Scott\2022");

        foreach (var subfodler in info.SubFolders)
            Console.WriteLine(subfodler);

        foreach (var parent in info.Parents)
            Console.WriteLine(parent);
    }

    [Test]
    public void Test_MultiFolder_Serialize()
    {
        var info = AbfMultiFolderInfo.FromPath(@"X:\Data\SD\practice\Scott\2022");

        var options = new JsonSerializerOptions() { WriteIndented = true };
        string json = JsonSerializer.Serialize(info, options);
        Console.WriteLine(json);

        var info2 = JsonSerializer.Deserialize<AbfMultiFolderInfo>(json);
    }
}