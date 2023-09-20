using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.Game.States;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private static GameBootstrapper _instance;
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        private void Awake()
        {
            if (_instance != null) { Destroy(gameObject); return; }
            DontDestroyOnLoad(gameObject);
            _instance = this;

            _gameStateMachine = new GameStateMachine(_sceneLoader);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}
