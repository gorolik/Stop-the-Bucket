using System.Collections;
using System.Linq;
using Sources.Extensions;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Services;
using Sources.Infrastructure.PersistentProgress.Structure;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.Analytics;
using Sources.Services.SceneData;
using Sources.UI.Factory;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class WinState : IPayloadState<int>
    {
        private const float _windowOpenDelay = 0.4f;
        
        private readonly ISceneDataService _sceneData;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressContainer _progressContainer;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameFactory _gameFactory;
        private readonly IAnalyticsService _analytics;


        public WinState(ISceneDataService sceneData, IPersistentProgressService persistentProgress, IUIFactory uiFactory,
            IPersistentProgressContainer progressContainer, ICoroutineRunner coroutineRunner, IGameFactory gameFactory,
            IAnalyticsService analytics)
        {
            _sceneData = sceneData;
            _persistentProgress = persistentProgress;
            _uiFactory = uiFactory;
            _progressContainer = progressContainer;
            _coroutineRunner = coroutineRunner;
            _gameFactory = gameFactory;
            _analytics = analytics;
        }

        public void Enter(int stars)
        {
            _gameFactory.GameEndListeners.ToArray().NotifyGameEndListeners();
            
            UpdateProgress(_progressContainer.PlayerProgress, stars);
            
            _coroutineRunner.StartCoroutine(OpenWindow(stars));
        }

        public void Exit() {}
        
        private IEnumerator OpenWindow(int stars)
        {
            yield return new WaitForSeconds(_windowOpenDelay);
            
            _uiFactory.CreateWinWindow(stars);
        }

        private void UpdateProgress(PlayerProgress progress, int stars)
        {
            _analytics.LevelPassed(_sceneData.LevelData.Id, stars);
            
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
            
            _analytics.LevelPassedFirst(_sceneData.LevelData.Id);
            
            _persistentProgress.SaveProgress();
        }
    }
}