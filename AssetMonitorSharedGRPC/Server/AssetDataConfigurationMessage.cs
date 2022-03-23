using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AssetMonitorSharedGRPC.Server
{
    [DataContract]
    public class AssetsDataConfigurationReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<AssetDataConfigurationMessage> AssetsData { get; set; }
    }

    [DataContract]
    public class AssetsDataConfigurationRequest
    {
        // If new configuration is needed on client side then false, if already loaded then true (empty response exptected)
        [DataMember(Order = 1)]        
        public bool NewConfigurationLoaded { get; set; }
    }

    [DataContract]
    public class AssetDataConfigurationMessage
    {
        [DataMember(Order = 1)]
        public int AssetId { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public string IPAddress { get; set; }

        [DataMember(Order = 4)]
        public IEnumerable<AssetTagConfigurationMessage> Tags { get; set; }

    }

    [DataContract]
    public class AssetTagConfigurationMessage
    {
        [DataMember(Order = 1)]
        public int TagId { get; set; }

        [DataMember(Order = 2)]
        public string Tagname { get; set; }

        [DataMember(Order = 3)]
        public double? RangeMax { get; set; }

        [DataMember(Order = 4)]
        public double? RangeMin { get; set; }
    }
}
