using System.Text.Json;

namespace ProjectServer.Shared;

public class AbfDayNotes
{
    public string Species { get; set; } = string.Empty;
    public string Strain { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string Genotype { get; set; } = string.Empty;
    public string DOB { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public string Rig { get; set; } = string.Empty;
    public string Experimenter { get; set; } = string.Empty;
    public string Internal { get; set; } = string.Empty;
    public string External { get; set; } = string.Empty;
    public string CageCard { get; set; } = string.Empty;
    public string Intervention { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public void SaveTxtFile(string filePath)
    {
        File.WriteAllText(filePath, GetTxt());
    }

    public string GetTxt()
    {
        System.Text.StringBuilder sb = new();
        AppendLineIfNotEmpty(sb, "species", Species);
        AppendLineIfNotEmpty(sb, "strain", Strain);
        AppendLineIfNotEmpty(sb, "date", Date);
        AppendLineIfNotEmpty(sb, "genotype", Genotype);
        AppendLineIfNotEmpty(sb, "dob", DOB);
        AppendLineIfNotEmpty(sb, "sex", Sex);
        AppendLineIfNotEmpty(sb, "rig", Rig);
        AppendLineIfNotEmpty(sb, "experimenter", Experimenter);
        AppendLineIfNotEmpty(sb, "internal", Internal);
        AppendLineIfNotEmpty(sb, "external", External);
        AppendLineIfNotEmpty(sb, "cagecard", CageCard);
        AppendLineIfNotEmpty(sb, "intervention", Intervention);
        return sb.ToString();
    }
    public static AbfDayNotes FromTxt(string txt)
    {
        System.Text.StringBuilder notes = new();

        AbfDayNotes day = new();
        foreach (string line in txt.Split("\n"))
        {
            if (!line.Contains(":"))
            {
                notes.AppendLine(line);
                continue;
            }

            string[] keyAndValue = line.Split(":", 2);
            string key = keyAndValue[0].Trim().ToLower();
            string value = keyAndValue[1].Trim();

            if (key == "species")
                day.Species = value;
            else if (key == "strain")
                day.Strain = value;
            else if (key == "date")
                day.Date = value;
            else if (key == "genotype")
                day.Genotype = value;
            else if (key == "dob")
                day.DOB = value;
            else if (key == "sex")
                day.Sex = value;
            else if (key == "rig")
                day.Rig = value;
            else if (key == "experimenter")
                day.Experimenter = value;
            else if (key == "internal")
                day.Internal = value;
            else if (key == "external")
                day.External = value;
            else if (key == "cagecard")
                day.CageCard = value;
            else if (key == "intervention")
                day.Intervention = value;
            else
                notes.AppendLine(line);
        }

        day.Notes = string.Join("\n", notes.ToString().Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)));

        return day;
    }

    public static AbfDayNotes LoadTxtFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);

        return FromTxt(File.ReadAllText(filePath));
    }

    private static void AppendLineIfNotEmpty(System.Text.StringBuilder sb, string name, string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            sb.AppendLine($"{name}: {value}");
    }

    public static ExperimentFolderInfo LoadJsonFile(string filePath)
    {
        return JsonSerializer.Deserialize<ExperimentFolderInfo>(File.ReadAllText(filePath))
            ?? throw new NullReferenceException();
    }

    public void SaveJsonFile(string filePath)
    {
        var options = new JsonSerializerOptions() { WriteIndented = true };
        string json = JsonSerializer.Serialize(this, options);
        File.WriteAllText(filePath, json);
    }
}
