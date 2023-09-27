using System;
using System.Collections.Generic;
using Sources.Behaviour.UI;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.Game.States;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.SceneData;
using Sources.Services.StaticData;
using Sources.UI.Factory;
using Zenject;

namespace Sources.Infrastructure.StateMachines.Game
{
    public class GameStateMachine : StateMachine, IGameStateMachine
    {
        public GameStateMachine(LevelStateMachine.Factory levelStateMachineFactory, SceneLoader sceneLoader,
            IGameFactory gameFactory, ISceneDataService sceneData, IStaticDataService staticData, Curtain curtain,
            IUIFactory uiFactory)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, staticData, curtain),
                [typeof(MainMenuState)] = new MainMenuState(sceneLoader, uiFactory, gameFactory, curtain),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, sceneData, gameFactory),
                [typeof(LevelLoopState)] = new LevelLoopState(levelStateMachineFactory),
            };
        }
        
        public class Factory
        {
            private readonly DiContainer _container;
            private readonly LevelStateMachine.Factory _levelStateMachineFactory;
            private readonly SceneLoader _sceneLoader;
            private readonly IGameFactory _gameFactory;
            private readonly ISceneDataService _sceneData;
            private readonly IStaticDataService _staticData;
            private readonly Curtain _curtain;
            private readonly IUIFactory _uiFactory;

            public Factory(
                DiContainer container, 
                LevelStateMachine.Factory levelStateMachineFactory,
                SceneLoader sceneLoader, 
                IGameFactory gameFactory, ISceneDataService sceneData, 
                IStaticDataService staticData, 
                Curtain curtain, 
                IUIFactory uiFactory)
            {
                _levelStateMachineFactory = levelStateMachineFactory;
                _container = container;
                _sceneLoader = sceneLoader;
                _gameFactory = gameFactory;
                _sceneData = sceneData;
                _staticData = staticData;
                _curtain = curtain;
                _uiFactory = uiFactory;
            }

            public GameStateMachine Create()
            {
                GameStateMachine gameStateMachine = new GameStateMachine(
                    _levelStateMachineFactory, 
                    _sceneLoader, 
                    _gameFactory, 
                    _sceneData, 
                    _staticData, 
                    _curtain, 
                    _uiFactory);
                
                _container.Bind<IGameStateMachine>()
                    .To<GameStateMachine>()
                    .FromInstance(gameStateMachine)
                    .AsSingle();

                return gameStateMachine;
            }
        }
    }
}