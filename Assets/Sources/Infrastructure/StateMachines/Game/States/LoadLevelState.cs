using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class LoadLevelState : IState
    {
        private const string _levelSceneName = "Level";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() => 
            _sceneLoader.Load(_levelSceneName, OnLevelLoaded);

        public void Exit() {}

        private void OnLevelLoaded() => 
            _gameStateMachine.Enter<LevelLoopState>();
    }
}