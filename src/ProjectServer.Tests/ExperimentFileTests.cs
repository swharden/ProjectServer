namespace ProjectServer.Tests;

internal class ExperimentFileTests
{
    [Test]
    public void Test_ExperimentFile_SaveAndLoad()
    {
        AbfExperimentFile day1 = new()
        {
            Species = "asdf",
            Strain = "qwer",
            Date = DateTime.Now.ToShortDateString(),
            Genotype = "zxcv",
            DOB = "5/3/2021",
            Sex = "M",
            Rig = "DIC2",
            Experimenter = "Bobbie",
            Internal = "K-glu (2 mM chloride)",
            External = "ACSF +DNQX/AP5",
            CageCard = "123456789",
            Intervention = "example intervention",
            Notes = "example notes\nhave multiple\nlines!",
        };

        string text1 = day1.GetText();
        Console.WriteLine(text1);

        AbfExperimentFile day2 = AbfExperimentFile.FromText(text1);

        Assert.That(day1.GetText(), Is.EqualTo(day2.GetText()));
    }

    [Test]
    public void Test_ExperimentFile_LoadExample()
    {
        string text = File.ReadAllText(Path.Combine(SampleData.Folder, "daily-experiment.txt"));
        AbfExperimentFile day = AbfExperimentFile.FromText(text);
        Console.WriteLine(day.GetText());
        Assert.Pass();
    }

    [Test]
    public void Test_ExperimentFile_Empty()
    {
        AbfExperimentFile day = AbfExperimentFile.FromText("minimal experiment file");
        Console.WriteLine(day.GetText());
        Assert.Pass();
    }
}
