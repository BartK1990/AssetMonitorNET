using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class SnmpTag : IEqualityComparer<SnmpTag>
    {
        public int Id { get; set; }

        [Required]
        public string OID { get; set; }

        [Required]
        public int OperationId { get; set; }
        public SnmpOperation Operation { get; set; }

        [Required]
        public int SnmpCommunicationTypeId { get; set; }
        public SnmpCommunicationType SnmpCommunicationType { get; set; }

        public TagCommunicationRel TagCommunicationRel { get; set; }

        public bool Equals(SnmpTag x, SnmpTag y)
        {
            if(x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(SnmpTag obj)
        {
            return this.Id.GetHashCode();
        }
    }
}
