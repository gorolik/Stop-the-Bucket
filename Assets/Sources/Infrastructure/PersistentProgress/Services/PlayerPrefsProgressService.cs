using Sources.Infrastructure.PersistentProgress.Structure;
using UnityEngine;

namespace Sources.Infrastructure.PersistentProgress.Services
{
    public class PlayerPrefsProgressService : PersistentProgressService
    {
        private const string _progressKey = "Progress";

        public PlayerPrefsProgressService(IPersistentProgressContainer progressContainer, IProgressListenersContainer progressListenersContainer) 
            : base(progressContainer, progressListenersContainer) {}

        protected override void Save()
        {
            PlayerPrefs.SetString(_progressKey, ProgressContainer.PlayerProgress.ToJson());
            PlayerPrefs.Save();
        }

        public override PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(_progressKey)?.Deserialize<PlayerProgress>();
    }
}