using Sources.Behaviour.UI;
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
        private readonly Curtain _curtain;

        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, ISceneDataService sceneData, Curtain curtain)
        {
            _curtain = curtain;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _sceneData = sceneData;
        }

        public void Enter(LevelData levelData)
        {
            _curtain.Show();
            
            _sceneData.Init(levelData);
            _sceneLoader.Load(_levelSceneName, OnLevelLoaded);
        }

        public void Exit() => 
            _curtain.Hide();

        private void OnLevelLoaded()
        {
            _sceneData.Load();
            _gameStateMachine.Enter<LevelLoopState>();
        }
    }
}