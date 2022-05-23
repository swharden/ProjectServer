namespace ProjectServer.Tests;

internal class AbfDayNotesTests
{
    [Test]
    public void Test_AbfDay_SaveAndLoad()
    {
        Shared.AbfDayNotes day1 = new()
        {
            Species = "Rat",
            Strain = "SD",
            Date = DateTime.Now.ToShortDateString(),
            Genotype = "GAD67-Cre",
            DOB = "5/3/2021",
            Sex = "M",
            Rig = "DIC2",
            Experimenter = "Bobbie",
            Internal = "K-glu (2 mM chloride)",
            External = "ACSF +DNQX/AP5",
            CageCard = "123456789",
            Intervention = "bilateral DIO-ChR2 NAc injection on 7/8/2022",
            Notes = "recordinds before 1PM are with the air table off",
        };

        day1.SaveTxtFile("test123.txt");


        Shared.AbfDayNotes day2 = Shared.AbfDayNotes.LoadTxtFile("test123.txt");

        Assert.That(day1.GetTxt(), Is.EqualTo(day2.GetTxt()));
    }
}
