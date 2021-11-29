using AspMVC_Monitor.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace TcpAgent
{
    class TcpHelper
    {
        public static void ListenAndResponse(TcpListener listener, int byteSize, object obj)
        {
            if (!listener.Server.IsBound)
            {
                Console.WriteLine("TcpListener is not started");
                return;
            }

            string message = null;
            byte[] buffer = new byte[byteSize];

            var sender = listener.AcceptTcpClient();
            Console.WriteLine($"Connection accepted from {sender.Client.RemoteEndPoint}");
            sender.GetStream().Read(buffer, 0, byteSize);

            // Read the message and perform different actions  
            message = CleanMessage(buffer);

            try // Check if message is valid
            {
                // Incoming message
                TcpExchangeInit tcpExchange = JsonConvert.DeserializeObject<TcpExchangeInit>(message); // Deserialize  
                if (tcpExchange.Init)
                {
                    byte[] bytes = System.Text.Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(obj));
                    SendMessage(bytes, sender);
                    Console.WriteLine($"Respond sent to {sender.Client.RemoteEndPoint}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static string CleanMessage(byte[] bytes)
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
        private static void SendMessage(byte[] bytes, TcpClient client)
        {
            client.GetStream()
                .Write(bytes, 0,
                bytes.Length); // Send the stream  
        }
    }
}
