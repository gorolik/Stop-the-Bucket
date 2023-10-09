using UnityEngine;

namespace Sources.Infrastructure.PersistentProgress.Services.DataSavers
{
    public class PlayerPrefsSaver : IDataSaver
    {
        private const string _progressKey = "Progress";
        
        public string GetData() => 
            PlayerPrefs.GetString(_progressKey);

        public void Save(string data) => 
            PlayerPrefs.SetString(_progressKey, data);
    }
}