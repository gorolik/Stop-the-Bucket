using System;
using System.Collections.Generic;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.SceneData;
using Sources.Services.Timer;
using Zenject;

namespace Sources.Infrastructure.StateMachines.Level
{
    public class LevelStateMachine : StateMachine, ILevelStateMachine
    {
        public LevelStateMachine(IGameFactory gameFactory, ITimersHandler timersHandler, ISceneDataService sceneDataService)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(CreateWorldState)] = new CreateWorldState(this, gameFactory, sceneDataService),
                [typeof(CountingState)] = new CountingState(this, timersHandler),
                [typeof(CatchingState)] = new CatchingState(gameFactory),
                [typeof(WinState)] = new WinState(),
                [typeof(LoseState)] = new LoseState(),
            };
        }
        
        public class Factory
        {
            private readonly IGameFactory _gameFactory;
            private readonly DiContainer _container;
            private readonly ITimersHandler _timersHandler;
            private readonly ISceneDataService _sceneDataService;

            public Factory(DiContainer container, IGameFactory gameFactory, ITimersHandler timersHandler, ISceneDataService sceneDataService)
            {
                _container = container;
                _gameFactory = gameFactory;
                _timersHandler = timersHandler;
                _sceneDataService = sceneDataService;
            }

            public LevelStateMachine Create()
            {
                LevelStateMachine levelStateMachine = new LevelStateMachine(_gameFactory, _timersHandler, _sceneDataService);
                
                _container.Bind<ILevelStateMachine>()
                    .To<LevelStateMachine>()
                    .FromInstance(levelStateMachine)
                    .AsSingle();
                
                return levelStateMachine;
            }
        }
    }
}