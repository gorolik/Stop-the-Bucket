using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.Game.States;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, IInitializable
    {
        private static GameBootstrapper _instance;
        
        private IGameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private IGameFactory _gameFactory;
        private GameStateMachine.Factory _gameStateMachineFactory;

        [Inject]
        public void Construct(GameStateMachine.Factory gameStateMachineFactory) => 
            _gameStateMachineFactory = gameStateMachineFactory;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Initialize()
        {
            _gameStateMachine = _gameStateMachineFactory.Create();
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}
