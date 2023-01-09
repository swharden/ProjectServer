namespace ProjectServer.Domain
{
    public struct AnalysisImagePath
    {
        public readonly string NextPath;
        public readonly string ThisPath;
        public readonly string PrevPath;

        public AnalysisImagePath(string nextPath, string thisPath, string prevPath)
        {
            NextPath = nextPath;
            ThisPath = thisPath;
            PrevPath = prevPath;
        }
    }
}