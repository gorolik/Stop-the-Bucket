using Sources.Infrastructure.StateMachines.States;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class BootstrapState : IState
    {
        private const string _initialSceneName = "Init";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            // curtain
            
            if (SceneManager.GetActiveScene().name != _initialSceneName)
                _sceneLoader.Load(_initialSceneName, OnInitSceneLoaded);
        }
        
        public void Exit() {}

        private void OnInitSceneLoaded() => 
            _gameStateMachine.Enter<LoadLevelState>();
    }
}