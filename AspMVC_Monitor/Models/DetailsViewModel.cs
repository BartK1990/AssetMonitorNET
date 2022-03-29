using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Models
{
    public class DetailsViewModel
    {
        public int? AssetId { get; set; }
        public ICollection<DetailsAsset> Assets { get; set; }
    }
}
