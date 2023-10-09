using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Services;
using Sources.Infrastructure.PersistentProgress.Structure;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.SceneData;
using Sources.UI.Factory;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class WinState : IPayloadState<int>
    {
        private readonly ISceneDataService _sceneData;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressContainer _progressContainer;

        public WinState(ISceneDataService sceneData, IPersistentProgressService persistentProgress, IUIFactory uiFactory,
            IPersistentProgressContainer progressContainer)
        {
            _sceneData = sceneData;
            _persistentProgress = persistentProgress;
            _uiFactory = uiFactory;
            _progressContainer = progressContainer;
        }

        public void Enter(int stars)
        {
            UpdateProgress(_progressContainer.PlayerProgress, stars);
            _uiFactory.CreateWinWindow(stars);
        }

        public void Exit() {}
        
        public void LoadProgress(PlayerProgress progress) {}

        public void UpdateProgress(PlayerProgress progress, int stars)
        {
            foreach (CompletedLevel level in progress.CompletedLevels)
            {
                if (level.Id == _sceneData.LevelData.Id)
                {
                    if(level.Stars < stars)
                        level.Stars = stars;
                    
                    _persistentProgress.SaveProgress();
                    
                    return;
                }
            }
            
            CompletedLevel completedLevel = new CompletedLevel(_sceneData.LevelData.Id, stars);
            progress.AddCompletedLevel(completedLevel);

            _persistentProgress.SaveProgress();
        }
    }
}