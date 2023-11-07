using System;
using System.Collections.Generic;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Services;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.SceneData;
using Sources.Services.Timer;
using Sources.UI.Factory;
using Zenject;

namespace Sources.Infrastructure.StateMachines.Level
{
    public class LevelStateMachine : StateMachine, ILevelStateMachine
    {
        public LevelStateMachine(IGameFactory gameFactory, ITimersHandler timersHandler, ISceneDataService sceneData,
            IPersistentProgressService progressService, IUIFactory uiFactory,
            IPersistentProgressContainer progressContainer, ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(CreateWorldState)] = new CreateWorldState(this, gameFactory, uiFactory, sceneData),
                [typeof(CountingState)] = new CountingState(this, timersHandler, uiFactory),
                [typeof(CatchingState)] = new CatchingState(gameFactory),
                [typeof(WinState)] = new WinState(sceneData, progressService, uiFactory, progressContainer, coroutineRunner, gameFactory),
                [typeof(LoseState)] = new LoseState(uiFactory, gameFactory, coroutineRunner),
            };
        }
        
        public class Factory
        {
            private readonly IGameFactory _gameFactory;
            private readonly DiContainer _container;
            private readonly ITimersHandler _timersHandler;
            private readonly ISceneDataService _sceneDataService;
            private readonly IPersistentProgressService _persistentProgress;
            private readonly IUIFactory _uiFactory;
            private readonly IPersistentProgressContainer _progressContainer;
            private readonly ICoroutineRunner _coroutineRunner;

            public Factory(
                DiContainer container, 
                IGameFactory gameFactory, 
                ITimersHandler timersHandler, 
                ISceneDataService sceneDataService, 
                IPersistentProgressService persistentProgress, 
                IUIFactory uiFactory,
                IPersistentProgressContainer progressContainer, ICoroutineRunner coroutineRunner)
            {
                _container = container;
                _gameFactory = gameFactory;
                _timersHandler = timersHandler;
                _sceneDataService = sceneDataService;
                _persistentProgress = persistentProgress;
                _uiFactory = uiFactory;
                _progressContainer = progressContainer;
                _coroutineRunner = coroutineRunner;
            }

            public LevelStateMachine Create()
            {
                LevelStateMachine levelStateMachine = new LevelStateMachine(
                    _gameFactory, 
                    _timersHandler, 
                    _sceneDataService, 
                    _persistentProgress, 
                    _uiFactory,
                    _progressContainer,
                    _coroutineRunner);
                
                _container.Bind<ILevelStateMachine>()
                    .To<LevelStateMachine>()
                    .FromInstance(levelStateMachine)
                    .AsCached();
                
                return levelStateMachine;
            }

            public void Cleanup() => 
                _container.Unbind<ILevelStateMachine>();
        }
    }
}