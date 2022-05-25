using ProjectServer.Shared;

namespace ProjectServer.Tests;

internal class ProjectFolderTests
{
    [Test]
    public void Test_Json_SaveAndLoad()
    {
        ProjectFolder project1 = new()
        {
            Created = DateTime.Now.AddMinutes(-5),
            Title = "test title",
            Description = "test description",
            Notes = "test notes",
            ExperimentFolders = new string[]
            {
                "subfolder 1",
                "subfolder 2",
                "subfolder 3",
            }
        };

        string testJsonFilePath = Path.GetFullPath("TestProject.json");
        Console.WriteLine(testJsonFilePath);
        if (File.Exists(testJsonFilePath))
            File.Delete(testJsonFilePath);

        project1.SaveJsonFile(testJsonFilePath);
        ProjectFolder project2 = ProjectFolder.LoadJsonFile(testJsonFilePath);

        Assert.That(project1.Title, Is.EqualTo(project2.Title));
        Assert.That(project1.Description, Is.EqualTo(project2.Description));
        Assert.That(project1.Notes, Is.EqualTo(project2.Notes));
        Assert.That(project1.ExperimentFolders, Is.EqualTo(project2.ExperimentFolders));
        Assert.That(project1.Created, Is.EqualTo(project2.Created));

        Assert.That(project1.Created, Is.LessThan(project1.Modified));
        Assert.That(project2.Created, Is.LessThan(project2.Modified));
    }

    [Test]
    public void Test_Project_Seed()
    {
        string folderPath = "./";
        ProjectFolder.SeedFolder(folderPath, "seed title", "seed description");
        ProjectFolder proj = ProjectFolder.Load(folderPath);

        Assert.That(proj.Title, Is.EqualTo("seed title"));
        Assert.That(proj.Description, Is.EqualTo("seed description"));
    }

    [Test]
    public void Test_Project_Load()
    {
        string folderPath = "./";
        ProjectFolder.SeedFolder(folderPath, "seed title", "seed description");
        ProjectFolder proj = ProjectFolder.Load(folderPath);
        Console.WriteLine(proj.FolderPath);
    }

    [Test]
    public void Test_Incomplete_Json()
    {
        string json = "{ \"Title\": \"test title\", \"Description\": \"test description\" }";

        ProjectFolder project = ProjectFolder.LoadJson(json);

        Assert.That(project.Title, Is.EqualTo("test title"));
        Assert.That(project.Description, Is.EqualTo("test description"));
    }
}
