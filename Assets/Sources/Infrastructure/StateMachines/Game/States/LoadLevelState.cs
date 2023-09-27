using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.SceneData;
using Sources.StaticData.Levels;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class LoadLevelState : IPayloadState<LevelData>
    {
        private const string _levelSceneName = "Level";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ISceneDataService _sceneData;
        private readonly IGameFactory _gameFactory;

        private LevelData _loadingLevelData;

        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, ISceneDataService sceneData, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _sceneData = sceneData;
            _gameFactory = gameFactory;
        }

        public void Enter(LevelData levelData)
        {
            _gameFactory.Cleanup();
            
            _loadingLevelData = levelData;
            _sceneLoader.Load(_levelSceneName, OnLevelLoaded);
        }

        public void Exit() {}

        private void OnLevelLoaded()
        {
            _sceneData.Load();
            _gameStateMachine.Enter<LevelLoopState, LevelData>(_loadingLevelData);
        }
    }
}