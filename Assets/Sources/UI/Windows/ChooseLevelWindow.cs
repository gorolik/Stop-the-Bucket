using Sources.Behaviour.UI;
using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.Game.States;
using Sources.Services.StaticData;
using Sources.StaticData.Levels;
using Sources.StaticData.Levels.Extensions;
using UnityEngine;
using Zenject;

namespace Sources.UI.Windows
{
    public class ChooseLevelWindow : WindowBase
    {
        [SerializeField] private LevelsMap _levelsMap;

        private IGameStateMachine _gameStateMachine;
        private IStaticDataService _staticData;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _staticData = staticData;
        }

        protected override void SubscribeUpdates() => 
            _levelsMap.LevelSelected += OnLevelSelected;

        protected override void Cleanup() => 
            _levelsMap.LevelSelected -= OnLevelSelected;

        private void OnLevelSelected(ClusterType cluster, int levelId)
        {
            LevelData levelData = _staticData.GetLevelsData().GetLevelDataByClusterAndId(cluster, levelId);
            _gameStateMachine.Enter<LoadLevelState, LevelData>(levelData);
        }
    }
}