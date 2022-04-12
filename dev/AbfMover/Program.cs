namespace AbfMover;

public static class Program
{
    public static void Main(string[] args)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        CellsTxt.BreakUp(@"C:\Users\swharden\Documents\temp\new\PFC");
        CellsTxt.BreakUp(@"C:\Users\swharden\Documents\temp\new\CA1");
        Console.WriteLine($"Finished in: {sw.Elapsed}");
    }
}