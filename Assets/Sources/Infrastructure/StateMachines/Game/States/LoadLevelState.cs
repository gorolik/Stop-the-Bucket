using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.SceneData;
using Sources.StaticData.Levels;
using Sources.UI.Factory;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class LoadLevelState : IPayloadState<LevelData>
    {
        private const string _levelSceneName = "Level";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ISceneDataService _sceneData;

        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, ISceneDataService sceneData)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _sceneData = sceneData;
        }

        public void Enter(LevelData levelData)
        {
            _sceneData.Init(levelData);
            
            _sceneLoader.Load(_levelSceneName, OnLevelLoaded);
        }

        public void Exit() {}

        private void OnLevelLoaded()
        {
            _sceneData.Load();
            _gameStateMachine.Enter<LevelLoopState>();
        }
    }
}