using System;
using System.Net.Sockets;

namespace AspMVC_Monitor.Models.Helpers
{
    public static class TcpHelper
    {
        public static byte[] SendMessage(int byteSize, byte[] messageBytes, string hostname, int tcpPort)
        {
            try // Try connecting and send the message bytes  
            {
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(hostname, tcpPort); // Create a new connection  
                NetworkStream stream = client.GetStream();

                stream.Write(messageBytes, 0, messageBytes.Length); // Write the bytes  

                messageBytes = new byte[byteSize]; // Clear the message   

                // Receive the stream of bytes  
                stream.Read(messageBytes, 0, messageBytes.Length);

                // Clean up  
                stream.Dispose();
                client.Close();
            }
            catch
            {
            }

            return messageBytes; // Return response  
        }

        public static byte[] SendMessageWithTimeLimit(int byteSize, byte[] messageBytes, string hostname, int tcpPort, TimeSpan timeLimit)
        {
            byte[] bArr = new byte[0];
            bool Completed = ExecuteHelper.ExecuteWithTimeLimit(timeLimit, () =>
            {
                bArr = SendMessage(byteSize, messageBytes, hostname, tcpPort);
            });

            return bArr;
        }
    }
}
