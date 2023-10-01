using UnityEngine;

namespace Sources.Infrastructure.PersistentProgress
{
    public static class DataExtensions
    {
        public static string ToJson(this  object obj) =>
            JsonUtility.ToJson(obj);

        public static T Deserialize<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}