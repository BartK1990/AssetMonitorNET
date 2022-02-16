﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AgentTag : IEqualityComparer<AgentTag>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Tagname { get; set; }

        public double ScaleFactor { get; set; }

        public double ScaleOffset { get; set; }

        public ICollection<HistoricalTagConfig> HistorizationTagConfigs { get; set; }
        
        [Required]
        public int AgentDataTypeId { get; set; }
        public AgentDataType AgentDataType { get; set; }

        [MaxLength(200)]
        public string PerformanceCounter { get; set; }

        [MaxLength(200)]
        public string WmiManagementObject { get; set; }

        [MaxLength(256)]
        public string ServiceName { get; set; }

        [Required]
        public int ValueDataTypeId { get; set; }
        public TagDataType ValueDataType { get; set; }

        [Required]
        public int AgentTagSetId { get; set; }
        public AgentTagSet AgentTagSet { get; set; }

        public bool Equals(AgentTag x, AgentTag y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(AgentTag obj)
        {
            return this.Id.GetHashCode();
        }
    }
}
