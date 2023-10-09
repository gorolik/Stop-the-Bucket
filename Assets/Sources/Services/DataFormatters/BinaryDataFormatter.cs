using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sources.Services.DataFormatters
{
    public class BinaryDataFormatter : IDataFormatter
    {
        public string Serialize(object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, obj);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public T Deserialize<T>(string data)
        {
            try
            {
                byte[] byteArray = Convert.FromBase64String(data);
                
                using (MemoryStream memoryStream = new MemoryStream(byteArray))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(memoryStream);
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
        }
    }
}