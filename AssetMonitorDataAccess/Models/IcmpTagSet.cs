using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssetMonitorDataAccess.Models
{
    public class IcmpTagSet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<IcmpTag> IcmpTag { get; set; }
    }
}
