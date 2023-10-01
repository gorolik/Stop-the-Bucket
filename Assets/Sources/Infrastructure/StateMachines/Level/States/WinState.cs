using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Structure;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.SceneData;
using Sources.UI.Factory;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class WinState : IPayloadState<int>, ISavedProgressUpdater
    {
        private readonly ISceneDataService _sceneData;
        private readonly ISaveLoadService _progressService;
        private readonly IUIFactory _uiFactory;

        private int _stars;

        public WinState(ISceneDataService sceneData, ISaveLoadService progressService, IUIFactory uiFactory)
        {
            _sceneData = sceneData;
            _progressService = progressService;
            _uiFactory = uiFactory;
        }

        public void Enter(int stars)
        {
            _stars = stars;
            
            _progressService.SaveProgress();
            _uiFactory.CreateWinWindow(_stars);
        }

        public void Exit() {}
        
        public void LoadProgress(PlayerProgress progress) {}

        public void UpdateProgress(PlayerProgress progress)
        {
            foreach (CompletedLevel level in progress.CompletedLevels)
            {
                if (level.Id == _sceneData.LevelData.Id)
                {
                    if(level.Stars < _stars)
                        level.Stars = _stars;
                    
                    return;
                }
            }
            
            CompletedLevel completedLevel = new CompletedLevel(_sceneData.LevelData.Id, _stars); // количество звёзд
            progress.CompletedLevels.Add(completedLevel);
        }
    }
}