namespace AssetMonitorService.Monitor.Model
{
    public abstract class TagValueBase
    {
        public readonly string Tagname;

        public TagValueBase(string tagname)
        {
            this.Tagname = tagname;
        }
    }
}
