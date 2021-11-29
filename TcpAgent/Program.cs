using AspMVC_Monitor.Model;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;

namespace TcpAgent
{
    class Program
    {
        public static AssetPerformanceData AssetPerformanceData;
        private static AssetPerformance _assetPerformance;

        private const int byteSize = 1024 * 1024;

        static void Main(string[] args)
        {
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
                string message = null;
                byte[] buffer = new byte[byteSize];

                var sender = listener.AcceptTcpClient();
                Console.WriteLine($"Connection accepted from {sender.Client.RemoteEndPoint}");
                sender.GetStream().Read(buffer, 0, byteSize);

                // Read the message and perform different actions  
                message = cleanMessage(buffer);

                try // Check if message is valid
                {
                    // Incoming message
                    TcpExchangeInit tcpExchange = JsonConvert.DeserializeObject<TcpExchangeInit>(message); // Deserialize  
                    if (tcpExchange.Init)
                    {
                        byte[] bytes = System.Text.Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(AssetPerformanceData));
                        sendMessage(bytes, sender);
                        Console.WriteLine($"Respond sent to {sender.Client.RemoteEndPoint}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static string cleanMessage(byte[] bytes)
        {
            string message = System.Text.Encoding.Unicode.GetString(bytes);

            string messageToPrint = null;
            foreach (var nullChar in message)
            {
                if (nullChar != '\0')
                {
                    messageToPrint += nullChar;
                }
            }
            return messageToPrint;
        }

        // Sends the message string using the bytes provided.  
        private static void sendMessage(byte[] bytes, TcpClient client)
        {
            client.GetStream()
                .Write(bytes, 0,
                bytes.Length); // Send the stream  
        }
    }
}
