using Sources.Behaviour.UI;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Services;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.LevelsStorage;
using Sources.Services.StaticData;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class BootstrapState : IState
    {
        private const string _initialSceneName = "Init";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly Curtain _curtain;
        private readonly ILevelsStorageService _levelsStorage;
        private readonly IPersistentProgressService _persistentProgress;

        public BootstrapState(
            IGameStateMachine gameStateMachine, 
            SceneLoader sceneLoader, 
            IStaticDataService staticData, 
            Curtain curtain, 
            ILevelsStorageService levelsStorage,
            IPersistentProgressService persistentProgress)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _curtain = curtain;
            _levelsStorage = levelsStorage;
            _persistentProgress = persistentProgress;
        }

        public void Enter()
        {
            _curtain.Show();
            
            _staticData.LoadData();
            _levelsStorage.Load();
            
            _sceneLoader.Load(_initialSceneName, OnInitSceneLoaded);
        }

        public void Exit() {}

        private void OnInitSceneLoaded() => 
            _gameStateMachine.Enter<LoadProgressState>();
    }
}