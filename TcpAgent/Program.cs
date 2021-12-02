using AspMVC_Monitor.Model;
using System;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace TcpAgent
{
    class Program
    {
        private static AssetPerformanceData AssetPerformanceData = new AssetPerformanceData();
        private static AssetPerformance _assetPerformance = new AssetPerformance();
        private static Timer _timer;

        static void Main(string[] args)
        {
            SetTimer(1000); // 1s

            IPEndPoint ep = new IPEndPoint(IPAddress.Any, TcpExchangeInit.TcpPort);
            TcpListener listener = new TcpListener(ep);
            listener.Start();

            Console.WriteLine(@"  
            ===================================================  
                   Started listening requests at: {0}:{1}  
            ===================================================",
            ep.Address, ep.Port);

            // Run the loop continuously; this is the server.  
            while (true)
            {
                TcpHelper.ListenAndResponse(listener, TcpExchangeInit.TcpMessagebByteSize, AssetPerformanceData);
                Console.WriteLine(AssetPerformanceData.ToString());
            }
        }

        private static void SetTimer(int time)
        {
            _timer = new Timer(time);
            _timer.Elapsed += OnTimer;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public static void OnTimer(object sender, ElapsedEventArgs args)
        {
            AssetPerformanceData = _assetPerformance.GetPerformanceData();
        }
    }
}
