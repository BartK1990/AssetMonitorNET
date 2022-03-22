namespace AspMVC_Monitor.Models
{
    public class TagLiveValue
    {
        public int Id { get; set; }
        public string Tagname { get; set; }
        public object Value { get; set; }
        public bool InAlarm { get; set; }
        public double? RangeMax { get; set; }
        public double? RangeMin { get; set; }
    }
}
