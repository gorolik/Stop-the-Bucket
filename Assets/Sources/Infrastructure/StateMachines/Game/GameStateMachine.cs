using System;
using System.Collections.Generic;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.Game.States;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.States;
using Zenject;

namespace Sources.Infrastructure.StateMachines.Game
{
    public class GameStateMachine : StateMachine, IGameStateMachine
    {
        public GameStateMachine(LevelStateMachine.Factory levelStateMachineFactory, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(LevelLoopState)] = new LevelLoopState(levelStateMachineFactory),
            };
        }
        
        public class Factory
        {
            private readonly DiContainer _container;
            private readonly LevelStateMachine.Factory _levelStateMachineFactory;
            private readonly SceneLoader _sceneLoader;
            private readonly IGameFactory _gameFactory;

            public Factory(
                DiContainer container, 
                LevelStateMachine.Factory levelStateMachineFactory,
                SceneLoader sceneLoader, 
                IGameFactory gameFactory)
            {
                _levelStateMachineFactory = levelStateMachineFactory;
                _container = container;
                _sceneLoader = sceneLoader;
                _gameFactory = gameFactory;
            }

            public GameStateMachine Create()
            {
                GameStateMachine gameStateMachine = new GameStateMachine(_levelStateMachineFactory, _sceneLoader, _gameFactory);
                
                _container.Bind<IGameStateMachine>()
                    .To<GameStateMachine>()
                    .FromInstance(gameStateMachine)
                    .AsSingle();

                return gameStateMachine;
            }
        }
    }
}