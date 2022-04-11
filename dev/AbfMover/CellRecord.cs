namespace AbfMover;

/// <summary>
/// Information about a cell (identified by its parent ABF)
/// Traditionally this was a single line in cells.txt
/// </summary>
public class CellRecord
{
    public string AbfID { get; init; }
    public string Color { get; init; }
    public string Comment { get; init; }
    public string Group { get; init; }

    public CellRecord(string abfID, string color, string comment, string group)
    {
        AbfID = abfID;
        Color = color;
        Comment = comment;
        Group = group;
    }

    public override string ToString()
    {
        return $"{AbfID} [{Group}] '{Comment}' ({Color})";
    }
}
