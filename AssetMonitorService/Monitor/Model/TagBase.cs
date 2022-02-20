namespace AssetMonitorService.Monitor.Model
{
    public abstract class TagBase
    {
        public readonly string Tagname;

        public TagBase(string tagname)
        {
            this.Tagname = tagname;
        }
    }
}
