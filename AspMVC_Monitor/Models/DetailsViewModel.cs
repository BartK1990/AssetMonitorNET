using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Models
{
    public class DetailsViewModel
    {
        public ICollection<DetailsAsset> Assets { get; set; }
    }
}
