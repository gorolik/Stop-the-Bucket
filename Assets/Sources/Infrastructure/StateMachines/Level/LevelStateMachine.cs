using System;
using System.Collections.Generic;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Infrastructure.StateMachines.States;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.StateMachines.Level
{
    public class LevelStateMachine : StateMachine, ILevelStateMachine
    {
        public LevelStateMachine(IGameFactory gameFactory)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(CreateWorldState)] = new CreateWorldState(this, gameFactory),
                [typeof(CountingState)] = new CountingState(this),
                [typeof(CatchingState)] = new CatchingState(gameFactory),
                [typeof(WinState)] = new WinState(),
                [typeof(LoseState)] = new LoseState(),
            };
        }
        
        public class Factory
        {
            private readonly IGameFactory _gameFactory;
            private readonly DiContainer _container;

            public Factory(DiContainer container, IGameFactory gameFactory)
            {
                _container = container;
                _gameFactory = gameFactory;
            }

            public LevelStateMachine Create()
            {
                LevelStateMachine levelStateMachine = new LevelStateMachine(_gameFactory);
                
                _container.Bind<ILevelStateMachine>()
                    .To<LevelStateMachine>()
                    .FromInstance(levelStateMachine)
                    .AsSingle();
                
                return levelStateMachine;
            }
        }
    }
}