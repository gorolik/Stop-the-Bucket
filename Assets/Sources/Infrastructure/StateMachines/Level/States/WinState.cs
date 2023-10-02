using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Structure;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.SceneData;
using Sources.UI.Factory;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class WinState : IPayloadState<int>, ISavedProgressUpdater
    {
        private readonly ISceneDataService _sceneData;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IUIFactory _uiFactory;

        private int _stars;

        public WinState(ISceneDataService sceneData, IPersistentProgressService persistentProgress, IUIFactory uiFactory)
        {
            _sceneData = sceneData;
            _persistentProgress = persistentProgress;
            _uiFactory = uiFactory;
        }

        public void Enter(int stars)
        {
            _stars = stars;
            
            _persistentProgress.SaveProgress();
            _uiFactory.CreateWinWindow(_stars);
        }

        public void Exit() {}
        
        public void LoadProgress(PlayerProgress progress) {}

        public void UpdateProgress(PlayerProgress progress)
        {
            Debug.Log("Progress updating");
            
            foreach (CompletedLevel level in progress.CompletedLevels)
            {
                if (level.Id == _sceneData.LevelData.Id)
                {
                    if(level.Stars < _stars)
                        level.Stars = _stars;
                    
                    return;
                }
            }
            
            CompletedLevel completedLevel = new CompletedLevel(_sceneData.LevelData.Id, _stars);
            progress.CompletedLevels.Add(completedLevel);
            Debug.Log("Added " + completedLevel.Id);
        }
    }
}