using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class UserEmailAddress
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int UserEmailAddressSetId { get; set; }
        public UserEmailAddressSet UserEmailAddressSet { get; set; }
    }
}
