using Sources.Behaviour.UI;
using Sources.Behaviour.UI.ChooseLevelMenu;
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

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, ILevelsStorageService levelsStorage)
        {
            _gameStateMachine = gameStateMachine;
            _levelsStorage = levelsStorage;
        }

        protected override void OnStart() => 
            _levelsMap.DisplayCluster(ClusterType.Beginner);

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