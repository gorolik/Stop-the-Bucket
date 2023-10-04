using System.IO;
using Sources.Infrastructure.PersistentProgress.Structure;
using UnityEngine;

namespace Sources.Infrastructure.PersistentProgress.Services
{
    public class FileProgressService : PersistentProgressService
    {
        private readonly string filePath = Application.persistentDataPath + "/Progress.save"; 
        
        public FileProgressService(IPersistentProgressContainer progressContainer, IProgressListenersContainer progressListenersContainer) 
            : base(progressContainer, progressListenersContainer) {}

        public override PlayerProgress LoadProgress()
        {
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                return data.Deserialize<PlayerProgress>();
            }

            return null;
        }

        protected override void Save()
        {
            File.WriteAllText(filePath, ProgressContainer.PlayerProgress.ToJson());
            Debug.Log(ProgressContainer.PlayerProgress.ToJson());
        }
    }
}