using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class IcmpTag : IEqualityComparer<IcmpTag>
    {
        public int Id { get; set; }

        [Required]
        public int IcmpTypeId { get; set; }
        public IcmpType IcmpType { get; set; }

        public TagCommunicationRel TagCommunicationRel { get; set; }

        public bool Equals(IcmpTag x, IcmpTag y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(IcmpTag obj)
        {
            return this.Id.GetHashCode();
        }
    }
}
