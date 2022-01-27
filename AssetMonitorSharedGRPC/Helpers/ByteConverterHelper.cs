using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AssetMonitorSharedGRPC.Helpers
{
    public static class ByteConverterHelper
    {
        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
