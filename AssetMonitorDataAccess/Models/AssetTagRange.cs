namespace AssetMonitorDataAccess.Models
{
    public class AssetTagRange
    {
        public int Id { get; set; }

        public double RangeMax { get; set; }
        public double RangeMin { get; set; }

        public int? AssetId { get; set; }
        public Asset Asset { get; set; }

        public int? TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
