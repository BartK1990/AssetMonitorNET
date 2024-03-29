﻿using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class UserEmailAssetRel
    {
        public int Id { get; set; }

        [Required]
        public int AssetId { get; set; }
        public Asset Asset { get; set; }

        [Required]
        public int UserEmailAddressSetId { get; set; }
        public UserEmailAddressSet UserEmailAddressSet { get; set; }
    }
}
