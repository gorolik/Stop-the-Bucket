using System;
using System.Collections.Generic;
using Sources.Behaviour.UI;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.StateMachines.Game.States;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.LevelsStorage;
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
            IUIFactory uiFactory, IPersistentProgressService persistentProgress, ILevelsStorageService levelsStorage)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, staticData, curtain, levelsStorage),
                [typeof(MainMenuState)] = new MainMenuState(sceneLoader, uiFactory, gameFactory, persistentProgress, curtain),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, sceneData),
                [typeof(LevelLoopState)] = new LevelLoopState(levelStateMachineFactory, gameFactory, uiFactory),
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
            private readonly IPersistentProgressService _persistentProgress;
            private readonly ILevelsStorageService _levelsStorage;

            public Factory(
                DiContainer container, 
                LevelStateMachine.Factory levelStateMachineFactory,
                SceneLoader sceneLoader, 
                IGameFactory gameFactory, ISceneDataService sceneData, 
                IStaticDataService staticData, 
                Curtain curtain, 
                IUIFactory uiFactory, IPersistentProgressService persistentProgress, ILevelsStorageService levelsStorage)
            {
                _levelStateMachineFactory = levelStateMachineFactory;
                _container = container;
                _sceneLoader = sceneLoader;
                _gameFactory = gameFactory;
                _sceneData = sceneData;
                _staticData = staticData;
                _curtain = curtain;
                _uiFactory = uiFactory;
                _persistentProgress = persistentProgress;
                _levelsStorage = levelsStorage;
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
                    _uiFactory, 
                    _persistentProgress,
                    _levelsStorage);
                
                _container.Bind<IGameStateMachine>()
                    .To<GameStateMachine>()
                    .FromInstance(gameStateMachine)
                    .AsSingle();

                return gameStateMachine;
            }
        }
    }
}