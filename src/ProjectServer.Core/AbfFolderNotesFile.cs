using System.Text;

namespace ProjectServer.Core;

/// <summary>
/// ABF Folders contain a daily notes file, "experiment.txt".
/// This class contains methods which interact with this file.
/// </summary>
public class AbfFolderNotesFile
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

    public string GetText()
    {
        static void AppendLineIfNotEmpty(StringBuilder sb, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                sb.AppendLine($"{name}: {value}");
        }

        StringBuilder sb = new();
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
        sb.AppendLine(Notes);
        return sb.ToString().Replace("\r\n","\n").Trim();
    }

    public static AbfFolderNotesFile FromFolder(string folder)
    {
        string filePath = Path.Combine(folder, "experiment.txt");

        return File.Exists(filePath) 
            ? FromFile(filePath) 
            : new AbfFolderNotesFile();
    }

    public static AbfFolderNotesFile FromFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);

        return FromText(File.ReadAllText(filePath));
    }

    public static AbfFolderNotesFile FromText(string txt)
    {
        StringBuilder notes = new();

        AbfFolderNotesFile day = new();
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
            else if (key == "cagecard" || key == "cage card")
                day.CageCard = value;
            else if (key == "intervention")
                day.Intervention = value;
            else
                notes.AppendLine(line.Trim());
        }

        day.Notes = string.Join("\n", notes.ToString().Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)));

        return day;
    }

    public void WriteFile(string filePath, bool createBackup = false)
    {
        if (createBackup && File.Exists(filePath))
        {
            string backupFilePath = filePath + $".backup.{DateTime.Now.Ticks}.txt";
            System.Diagnostics.Debug.WriteLine($"Creating backup: {backupFilePath}");
            File.Copy(filePath, backupFilePath);
        }

        File.WriteAllText(filePath, GetText());
        System.Diagnostics.Debug.WriteLine($"Saving: {filePath}");
    }
}
