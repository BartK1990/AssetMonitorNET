﻿using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class SnmpOperation
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Operation { get; set; }
    }
}
