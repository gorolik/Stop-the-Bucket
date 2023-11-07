using Sources.Behaviour.UI.ChooseLevelMenu;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.Game.States;
using Sources.Services.LevelsStorage;
using Sources.StaticData.Levels;
using UnityEngine;
using Zenject;

namespace Sources.UI.Windows
{
    public class ChooseLevelWindow : WindowBase
    {
        [SerializeField] private LevelsMap _levelsMap;

        private IGameStateMachine _gameStateMachine;
        private ILevelsStorageService _levelsStorage;
        private IPersistentProgressContainer _progressContainer;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, ILevelsStorageService levelsStorage, IPersistentProgressContainer progressContainer)
        {
            _gameStateMachine = gameStateMachine;
            _levelsStorage = levelsStorage;
            _progressContainer = progressContainer;
        }

        protected override void OnStart() => 
            _levelsMap.DisplayCluster(_progressContainer.PlayerProgress.SelectedCluster);

        protected override void SubscribeUpdates() => 
            _levelsMap.LevelSelected += OnLevelSelected;

        protected override void Cleanup() => 
            _levelsMap.LevelSelected -= OnLevelSelected;

        private void OnLevelSelected(int levelId)
        {
            LevelData levelData = _levelsStorage.LevelsData[levelId];
            
            _gameStateMachine.Enter<LoadLevelState, LevelData>(levelData);
        }
    }
}