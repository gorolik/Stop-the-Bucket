using Sources.Infrastructure.StateMachines.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class BootstrapState : IState
    {
        private const string _initialSceneName = "Init";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            // curtain
            _sceneLoader.Load(_initialSceneName, OnInitSceneLoaded);
        }
        
        public void Exit() {}

        private void OnInitSceneLoaded() => 
            _gameStateMachine.Enter<LoadLevelState>();
    }
}