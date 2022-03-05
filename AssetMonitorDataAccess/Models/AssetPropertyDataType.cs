﻿using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AssetPropertyDataType
    {
        public int Id { get; set; }

        [Required]
        public string DataType { get; set; }
    }
}
