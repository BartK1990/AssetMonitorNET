using AssetMonitorService.Monitor.Model;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public class AssetSnmpDataService : IAssetSnmpDataService
    {
        private readonly ILogger<AssetSnmpDataService> _logger;

        public AssetSnmpDataService(ILogger<AssetSnmpDataService> logger)
        {
            this._logger = logger;
        }

        public async Task UpdateAsset(AssetSnmpData assetData)
        {
            var version = assetData.Version switch
            {
                AssetMonitorDataAccess.Models.Enums.SnmpVersionEnum.V1 => VersionCode.V1,
                AssetMonitorDataAccess.Models.Enums.SnmpVersionEnum.V2c => VersionCode.V2,
                _ => throw new ArgumentException("Wrong SNMP version configured"),
            };

            var ipEndPoint = new IPEndPoint(IPAddress.Parse(assetData.IpAddress), assetData.UdpPort);
            var community = new OctetString(assetData.Community);
            var variables = (assetData.Data.Keys.Select(ak => new Variable(new ObjectIdentifier(ak.OID)))).ToList();


            //var snmpResult = Messenger.Get(version, ipEndPoint, community, variables, assetData.Timeout);
            var snmpResult = await Messenger.GetAsync(version, ipEndPoint, community, variables);

            foreach (var d in assetData.Data)
            {
                var val = snmpResult.Where(sr => sr.Id.ToString() == d.Key.OID).FirstOrDefault();
                if (val!=null)
                {
                    d.Value.Value = val.Data.ToString();
                }
            }
        }
    }
}
