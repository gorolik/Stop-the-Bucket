using UnityEngine;

namespace Sources.Services.DataFormatters
{
    public class JsonDataFormatter : IDataFormatter
    {
        public string Serialize(object obj) =>
            JsonUtility.ToJson(obj);
        
        public T Deserialize<T>(string data) =>
            JsonUtility.FromJson<T>(data);
    }
}