using Sources.Behaviour.UI;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Services;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.Ads;
using Sources.Services.LevelsStorage;
using Sources.Services.Localization;
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
        private readonly IAdsService _adsService;
        private readonly Localizator _localizator;

        public BootstrapState(
            IGameStateMachine gameStateMachine, 
            SceneLoader sceneLoader, 
            IStaticDataService staticData, 
            Curtain curtain, 
            ILevelsStorageService levelsStorage,
            IPersistentProgressService persistentProgress,
            IAdsService adsService,
            Localizator localizator)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _curtain = curtain;
            _levelsStorage = levelsStorage;
            _persistentProgress = persistentProgress;
            _adsService = adsService;
            _localizator = localizator;
        }

        public void Enter()
        {
            _localizator.Init();
            
            _curtain.Show();

            _staticData.LoadData();
            _levelsStorage.Load();
            _adsService.Init();

            _sceneLoader.Load(_initialSceneName, OnInitSceneLoaded);
        }

        public void Exit() {}

        private void OnInitSceneLoaded() => 
            _gameStateMachine.Enter<LoadProgressState>();
    }
}