using AspMVC_Monitor.Model;
using AspMVC_Monitor.Models.Helpers;
using AssetMonitorDataAccess.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Models
{
    public class AssetHolder : IAssetHolder
    {
        private readonly IServiceScopeFactory _scopeFactory;  
        private AssetPerformance _assetPerformance;

        public List<Asset> AssetList { get; set; }

        public AssetHolder(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
            AssetList = new List<Asset>();

            UpdateAssetsList();
            _assetPerformance = new AssetPerformance();
        }

        public void UpdateAssetsList()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbCtx = scope.ServiceProvider.GetRequiredService<AssetMonitorContext>();
                var assets = dbCtx.Assets.ToList();
                foreach (var a in assets)
                {
                    if (AssetList.Select(a => a.Id).Contains(a.Id))
                    {
                        continue;
                    }
                    AssetList.Add(new Asset()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        IpAddress = a.IpAddress
                    });
                }
            }
        }

        public void AddAsset(string name, string ipAddress)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(ipAddress))
                return;

            if (!IPAddress.TryParse(ipAddress, out _))
                return;

            if (AssetList.Select(n => n.Name).ToList().Contains(name))
                return;

            if (AssetList.Select(n => n.IpAddress).ToList().Contains(ipAddress))
                return;

            AssetList.Add(
                new Asset()
                {
                    Name = name,
                    IpAddress = ipAddress,
                });
        }

        public void UpdateAssetPing()
        {
            foreach (var a in AssetList)
            {
                a.PingState = PingHelper.PingHostWithTimeLimit(
                    a.IpAddress, 
                    out var pingResponseTime, 
                    TimeSpan.FromMilliseconds(4000));
                a.PingResponseTime = pingResponseTime;
            }
        }
        public async Task UpdateAssetPingAsync()
        {
            await Task.Run(() => UpdateAssetPing());
        }

        public void UpdateAssetPerformance()
        {
            foreach (var a in AssetList)
            {
                if (a.IpAddress == IPAddress.Loopback.ToString())
                {
                    a.AssignPerformanceData(_assetPerformance.GetPerformanceData());
                }
                else
                {
                    var tei = new TcpExchangeInit(true);
                    byte[] bytes = TcpHelper.SendMessageWithTimeLimit(
                        TcpExchangeInit.TcpMessagebByteSize,
                        System.Text.Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(tei)),
                        a.IpAddress,
                        TcpExchangeInit.TcpPort,
                        TimeSpan.FromMilliseconds(4000));
                    try
                    {
                        AssetPerformanceData assetPData = JsonConvert.DeserializeObject<AssetPerformanceData>(System.Text.Encoding.Unicode.GetString(bytes));
                        a.AssignPerformanceData(assetPData);
                    }
                    catch
                    {
                    }
                }
            }
        }
        public async Task UpdateAssetPerformanceAsync()
        {
            await Task.Run(() => UpdateAssetPerformance());
        }

    }
}
