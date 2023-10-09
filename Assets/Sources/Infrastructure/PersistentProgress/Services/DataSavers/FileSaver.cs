using System.IO;
using UnityEngine;

namespace Sources.Infrastructure.PersistentProgress.Services.DataSavers
{
    public class FileSaver : IDataSaver
    {
        private readonly string _filePath = Application.persistentDataPath + "/Progress.save"; 
        
        public string GetData()
        {
            string data = string.Empty;
            
            if (File.Exists(_filePath)) 
                data = File.ReadAllText(_filePath);

            return data;
        }

        public void Save(string data) => 
            File.WriteAllText(_filePath, data);
    }
}