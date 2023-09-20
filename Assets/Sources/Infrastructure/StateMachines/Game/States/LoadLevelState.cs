using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class LoadLevelState : IState
    {
        private const string _levelSceneName = "Level";

        private readonly IStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(IStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() => 
            _sceneLoader.Load(_levelSceneName, OnLevelLoaded);

        public void Exit() {}

        private void OnLevelLoaded() => 
            _stateMachine.Enter<LevelLoopState>();
    }
}