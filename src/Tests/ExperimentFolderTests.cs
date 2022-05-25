using ProjectServer.Shared;

namespace ProjectServer.Tests;

internal class ExperimentFolderTests
{
    [Test]
    public void Test_Experiment_Load()
    {
        ExperimentFolder exp = new()
        {
            Title = "test title",
            Description = "test description",
            Notes = "test notes",
        };

        string jsonFilePath = Path.GetFullPath("test-exp.json");

        exp.SaveJsonFile(jsonFilePath);

        ExperimentFolder exp2 = ExperimentFolder.LoadJsonFile(jsonFilePath);

        Assert.That(exp.Title, Is.EqualTo(exp2.Title));
        Assert.That(exp.Description, Is.EqualTo(exp2.Description));
        Assert.That(exp.Notes, Is.EqualTo(exp2.Notes));
    }
}
