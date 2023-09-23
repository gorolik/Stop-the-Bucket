using System;
using System.Collections.Generic;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.Timer;
using Zenject;

namespace Sources.Infrastructure.StateMachines.Level
{
    public class LevelStateMachine : StateMachine, ILevelStateMachine
    {
        public LevelStateMachine(IGameFactory gameFactory, ITimersHandler timersHandler)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(CreateWorldState)] = new CreateWorldState(this, gameFactory),
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

            public Factory(DiContainer container, IGameFactory gameFactory, ITimersHandler timersHandler)
            {
                _container = container;
                _gameFactory = gameFactory;
                _timersHandler = timersHandler;
            }

            public LevelStateMachine Create()
            {
                LevelStateMachine levelStateMachine = new LevelStateMachine(_gameFactory, _timersHandler);
                
                _container.Bind<ILevelStateMachine>()
                    .To<LevelStateMachine>()
                    .FromInstance(levelStateMachine)
                    .AsSingle();
                
                return levelStateMachine;
            }
        }
    }
}