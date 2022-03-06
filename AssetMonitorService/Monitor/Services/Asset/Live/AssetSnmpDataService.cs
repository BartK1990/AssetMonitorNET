using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorService.Monitor.Model.TagConfig;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services.Asset.Live
{
    public class AssetSnmpDataService : IAssetSnmpDataService
    {
        private readonly ILogger<AssetSnmpDataService> _logger;

        public AssetSnmpDataService(ILogger<AssetSnmpDataService> logger)
        {
            _logger = logger;
        }

        public async Task UpdateAsset(AssetSnmpData assetData)
        {
            var keys = assetData.Data.Keys.Where(k => k.CommunicationType == SnmpCommunicationTypeEnum.Normal);
            UpdateAssetSelectedVariables(assetData, keys);
        }

        public async Task UpdateAssetOnDemandData(AssetSnmpData assetData)
        {
            var keys = assetData.Data.Keys.Where(k => k.CommunicationType == SnmpCommunicationTypeEnum.OnDemand);
            UpdateAssetSelectedVariables(assetData, keys);
        }

        private void UpdateAssetSelectedVariables(AssetSnmpData assetData, IEnumerable<TagSnmp> tags)
        {
            var variables = tags.Select(ak => new Variable(new ObjectIdentifier(ak.OID))).ToList();

            var version = assetData.Version switch
            {
                SnmpVersionEnum.V1 => VersionCode.V1,
                SnmpVersionEnum.V2 => VersionCode.V2,
                _ => throw new ArgumentException("Wrong SNMP version configured"),
            };

            var ipEndPoint = new IPEndPoint(IPAddress.Parse(assetData.IpAddress), assetData.UdpPort);
            var community = new OctetString(assetData.Community);

            try
            {
                var snmpResult = Messenger.Get(version, ipEndPoint, community, variables, assetData.Timeout);

                foreach (var tagSnmp in tags)
                {
                    var val = snmpResult.Where(sr => sr.Id.ToString() == tagSnmp.OID).FirstOrDefault();
                    if (val != null)
                    {
                        assetData.Data[tagSnmp].Value = val.Data.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                foreach (var tagSnmp in tags) // Assing null values if error
                {
                    assetData.Data[tagSnmp].Value = null;
                }
                _logger.LogWarning($"Cannot retrieve data with SNMP: {assetData.IpAddress}:{assetData.UdpPort}");
                _logger.LogDebug($"Exception: { ex.Message}");
            }
        }
    }
}
