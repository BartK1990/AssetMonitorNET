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

        public static object ByteArrayToObject(byte[] arrBytes)
        {
            if (arrBytes == null)
                return null;

            BinaryFormatter binForm = new BinaryFormatter();
            using(MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                object obj = (object)binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}
