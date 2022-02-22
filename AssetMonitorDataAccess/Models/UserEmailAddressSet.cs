using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class UserEmailAddressSet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<UserEmailAddress> UserEmailAddresses { get; set; }
    }
}
