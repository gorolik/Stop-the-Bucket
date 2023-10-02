using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress.Structure;
using UnityEngine;

namespace Sources.Infrastructure.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private const string _progressKey = "Progress";

        private readonly IPersistentProgressContainer _progressContainer;
        private readonly IProgressListenersContainer _progressListenersContainer;

        public PersistentProgressService(IPersistentProgressContainer progressContainer, IProgressListenersContainer progressListenersContainer)
        {
            _progressContainer = progressContainer;
            _progressListenersContainer = progressListenersContainer;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgressUpdater progressUpdater in _progressListenersContainer.SavedProgressUpdaters)
                progressUpdater.UpdateProgress(_progressContainer.PlayerProgress);

            PlayerPrefs.SetString(_progressKey, _progressContainer.PlayerProgress.ToJson());
            PlayerPrefs.Save();
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(_progressKey)?.Deserialize<PlayerProgress>();
    }
}