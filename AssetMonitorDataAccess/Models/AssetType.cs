using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssetMonitorDataAccess.Models
{
    public class AssetType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Type { get; set; }

    }
}
