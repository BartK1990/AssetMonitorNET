using System.Collections.Generic;

namespace AspMVC_Monitor.Controllers.Json
{
    public class assetsJson
    {
        public int okCnt { get; set; }
        public int inAlarmCnt { get; set; }

        public List<assetJson> assets { get; set; }
    }
}
