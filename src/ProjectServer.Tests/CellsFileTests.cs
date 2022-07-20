namespace ProjectServer.Tests;

public class Tests
{
    [Test]
    public void Test_CellsFileLine_AbfIdOnly()
    {
        string line = "22_01_02_0123   ";
        CellsFileAbf abf = CellsFile.ParseLine(line);

        Assert.That(abf.AbfID, Is.EqualTo("22_01_02_0123"));
    }

    [Test]
    public void Test_CellsFileLine_AbfIdAndCustomColor()
    {
        string line = "22_01_02_0123 samplecolor   ";
        CellsFileAbf abf = CellsFile.ParseLine(line);

        Assert.That(abf.AbfID, Is.EqualTo("22_01_02_0123"));
        Assert.That(abf.Color, Is.EqualTo("samplecolor"));
    }

    [Test]
    public void Test_CellsFileLine_AbfIdAndHexColor()
    {
        string line = "22_01_02_0123 #003366   ";
        CellsFileAbf abf = CellsFile.ParseLine(line);

        Assert.That(abf.AbfID, Is.EqualTo("22_01_02_0123"));
        Assert.That(abf.Color, Is.EqualTo("#003366"));
    }

    [Test]
    public void Test_CellsFileLine_AbfIdColorCode()
    {
        string line = "22_01_02_0123 g1   ";
        CellsFileAbf abf = CellsFile.ParseLine(line);

        Assert.That(abf.AbfID, Is.EqualTo("22_01_02_0123"));
        Assert.That(abf.Color, Is.EqualTo("#00CC00"));
    }

    [Test]
    public void Test_CellsFileLine_Traditional()
    {
        string line = "22_01_02_0123 g1 example comment with spaces   ";
        CellsFileAbf abf = CellsFile.ParseLine(line);

        Assert.That(abf.AbfID, Is.EqualTo("22_01_02_0123"));
        Assert.That(abf.Color, Is.EqualTo("#00CC00"));
        Assert.That(abf.Comment, Is.EqualTo("example comment with spaces"));
    }

    [Test]
    public void Test_CellsFileLine_Tags()
    {
        string line = "22_01_02_0123 g1 example comment with spaces !TAG: tag one !TAG: tag two !TAG: tag three  ";
        CellsFileAbf abf = CellsFile.ParseLine(line);

        Assert.That(abf.AbfID, Is.EqualTo("22_01_02_0123"));
        Assert.That(abf.Color, Is.EqualTo("#00CC00"));
        Assert.That(abf.Comment, Is.EqualTo("example comment with spaces"));
        Assert.That(abf.Tags, Has.Length.EqualTo(3));
        Assert.That(abf.Tags[0], Is.EqualTo("tag one"));
        Assert.That(abf.Tags[1], Is.EqualTo("tag two"));
        Assert.That(abf.Tags[2], Is.EqualTo("tag three"));
    }

    [Test]
    public void Test_CellsLineFromAbfInfo_ColorLookup()
    {
        string line = "22_01_02_0123 g1 example comment with spaces";

        CellsFileAbf abf = new()
        {
            AbfID = "22_01_02_0123",
            Color = "#00CC00",
            Comment = "example comment with spaces"
        };

        Assert.That(CellsFile.CreateLine(abf), Is.EqualTo(line));
    }

    [Test]
    public void Test_CellsLineFromAbfInfo_WithoutTags()
    {
        string line = "22_01_02_0123 g1 example comment with spaces";

        CellsFileAbf abf = new()
        {
            AbfID = "22_01_02_0123",
            Color = "g1",
            Comment = "example comment with spaces"
        };

        Assert.That(CellsFile.CreateLine(abf), Is.EqualTo(line));
    }

    [Test]
    public void Test_CellsLineFromAbfInfo_WithTags()
    {
        string line = "22_01_02_0123 g1 example comment with spaces !TAG: tag one !TAG: tag two !TAG: tag three";

        CellsFileAbf abf = new()
        {
            AbfID = "22_01_02_0123",
            Color = "g1",
            Comment = "example comment with spaces",
            Tags = new string[] { "tag one", "tag two", "tag three" }
        };

        Assert.That(CellsFile.CreateLine(abf), Is.EqualTo(line));
    }

    [Test]
    public void Test_CellsFile_ReadSampleFile()
    {
        string text = File.ReadAllText(Path.Combine(SampleData.Folder, "sample-cells.txt"));
        CellsFile cells = new(text);

        Assert.That(cells.Lookup("badAbfID"), Is.Null);
        Assert.That(cells.Lookup("2022_01_03_0020"), Is.Not.Null);

        CellsFileAbf abf = cells.Lookup("2022_01_03_0020")!;
        Assert.That(abf.AbfID, Is.EqualTo("2022_01_03_0020"));
        Assert.That(abf.Heading, Is.EqualTo("heading 1"));
        Assert.That(abf.Color, Is.EqualTo("#00CC00"));
        Assert.That(abf.Comment, Is.EqualTo("long example comment"));
        Assert.That(abf.Tags, Has.Length.EqualTo(3));
        Assert.That(abf.Tags[0], Is.EqualTo("tag one"));
        Assert.That(abf.Tags[1], Is.EqualTo("tag two"));
        Assert.That(abf.Tags[2], Is.EqualTo("tag three"));
    }

    [Test]
    public void Test_CellsFile_ReadLargeFile()
    {
        string text = File.ReadAllText(Path.Combine(SampleData.Folder, "large-cells.txt"));
        CellsFile cells = new(text);
        Assert.Pass();
    }

    [Test]
    public void Test_CellsFile_Update()
    {
        string text = File.ReadAllText(Path.Combine(SampleData.Folder, "sample-cells.txt"));
        CellsFile cells = new(text);
        cells.Update("2022_01_03_0020", "#123123", "updated comment", new string[] { "new1", "new2" });
        Assert.That(cells.Lookup("2022_01_03_0020"), Is.Not.Null);
        Assert.That(cells.Lookup("2022_01_03_0020")!.AbfID, Is.EqualTo("2022_01_03_0020"));
        Assert.That(cells.Lookup("2022_01_03_0020")!.Color, Is.EqualTo("#123123"));
        Assert.That(cells.Lookup("2022_01_03_0020")!.Comment, Is.EqualTo("updated comment"));
        Assert.That(cells.Lookup("2022_01_03_0020")!.Tags, Has.Length.EqualTo(2));
        Assert.That(cells.Lookup("2022_01_03_0020")!.Tags[0], Is.EqualTo("new1"));
        Assert.That(cells.Lookup("2022_01_03_0020")!.Tags[1], Is.EqualTo("new2"));
    }

    [Test]
    public void Test_CellsFile_Add()
    {
        string text = File.ReadAllText(Path.Combine(SampleData.Folder, "sample-cells.txt"));
        CellsFile cells = new(text);
        cells.Update("2022_01_03_9999", "#123123", "updated comment", new string[] { "new1", "new2" });
        Assert.That(cells.Lookup("2022_01_03_9999"), Is.Not.Null);
        Assert.That(cells.Lookup("2022_01_03_9999")!.AbfID, Is.EqualTo("2022_01_03_9999"));
        Assert.That(cells.Lookup("2022_01_03_9999")!.Color, Is.EqualTo("#123123"));
        Assert.That(cells.Lookup("2022_01_03_9999")!.Comment, Is.EqualTo("updated comment"));
        Assert.That(cells.Lookup("2022_01_03_9999")!.Tags, Has.Length.EqualTo(2));
        Assert.That(cells.Lookup("2022_01_03_9999")!.Tags[0], Is.EqualTo("new1"));
        Assert.That(cells.Lookup("2022_01_03_9999")!.Tags[1], Is.EqualTo("new2"));
    }
}