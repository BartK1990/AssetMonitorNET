﻿using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class TagDataType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string DataType { get; set; }
    }
}
