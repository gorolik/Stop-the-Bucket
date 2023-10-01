using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress.Structure;
using UnityEngine;

namespace Sources.Infrastructure.PersistentProgress
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string _progressKey = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgressUpdater progressUpdater in _gameFactory.SavedProgressUpdaters)
                progressUpdater.UpdateProgress(_progressService.PlayerProgress);

            PlayerPrefs.SetString(_progressKey, _progressService.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(_progressKey)?.Deserialize<PlayerProgress>();
    }
}