using System.Collections.Generic;

namespace AspMVC_Monitor.Controllers.Json
{
    public class assetJson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string ipAddress { get; set; }
        public bool inAlarm { get; set; }

        public List<assetTagJson> tags { get; set; }
    }
}
