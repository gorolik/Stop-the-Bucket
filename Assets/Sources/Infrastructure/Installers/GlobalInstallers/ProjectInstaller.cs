using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.GameInstallSettings;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Services;
using Sources.Infrastructure.PersistentProgress.Services.DataSavers;
using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Services.Ads;
using Sources.Services.DataFormatters;
using Sources.Services.Input;
using Sources.Services.LevelResult;
using Sources.Services.LevelsStorage;
using Sources.Services.SceneData;
using Sources.Services.StaticData;
using Sources.Services.Timer;
using Sources.UI.Factory;
using Sources.UI.Services;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Installers.GlobalInstallers
{
    public class ProjectInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private InstallData _installData;

        public override void InstallBindings()
        {
            BindProgressListenersContainer();
            BindCoroutineRunner();
            BindTimersHandler();
            BindStaticDataService();
            BindSceneLoader();
            BindInputService();
            BindAssetProvider();
            BindDataFormatter();
            BindDataSaver();
            BindPersistentProgressService();
            BindPersistentProgressContainer();
            BindLevelsStorageService();
            BindWindowService();
            BindGameFactory();
            BindUIFactory();
            BindSceneDataService();
            BindLevelResultService();
            BindAdsService();
            BindLevelStateMachineFactory();
            BindGameStateMachineFactory();
        }

        private void BindProgressListenersContainer() =>
            Container.Bind<IProgressListenersContainer>()
                .To<ProgressListenersContainer>()
                .AsSingle();

        private void BindCoroutineRunner() =>
            Container.Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle();

        private void BindTimersHandler() =>
            Container.Bind<ITimersHandler>()
                .To<TimersHandler>()
                .AsSingle();

        private void BindStaticDataService() =>
            Container.Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle();

        private void BindSceneLoader() =>
            Container.Bind<SceneLoader>()
                .AsSingle();

        private void BindInputService()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
                Container.Bind<IInputService>()
                    .To<DesktopInputService>()
                    .AsSingle();
            else if(SystemInfo.deviceType == DeviceType.Handheld)
                Container.Bind<IInputService>()
                    .To<MobileInputService>()
                    .AsSingle();
            else
                Debug.LogError("Not implement input service");
        }

        private void BindDataFormatter() =>
            Container.Bind<IDataFormatter>()
                .To<JsonDataFormatter>()
                .AsSingle();

        private void BindDataSaver()
        {
            if (_installData.TargetPlatform is TargetPlatform.Itch or TargetPlatform.YandexGames)
                Container.Bind<IDataSaver>()
                    .To<PlayerPrefsSaver>()
                    .AsSingle();
            else
                Container.Bind<IDataSaver>()
                    .To<FileSaver>()
                    .AsSingle();
        }

        private void BindAssetProvider() =>
            Container.Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();

        private void BindPersistentProgressService() =>
            Container.Bind<IPersistentProgressService>()
                .To<PersistentProgressService>()
                .AsSingle();

        private void BindPersistentProgressContainer() =>
            Container.Bind<IPersistentProgressContainer>()
                .To<PersistentProgressContainer>()
                .AsSingle();

        private void BindLevelsStorageService() =>
            Container.Bind<ILevelsStorageService>()
                .To<LevelsStorageService>()
                .AsSingle();

        private void BindWindowService() =>
            Container.Bind<IWindowService>()
                .To<WindowService>()
                .AsSingle();

        private void BindGameFactory() =>
            Container.Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();

        private void BindUIFactory() =>
            Container.Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();

        private void BindSceneDataService() =>
            Container.Bind<ISceneDataService>()
                .To<SceneDataService>()
                .AsSingle();

        private void BindLevelResultService() =>
            Container.Bind<ILevelResultService>()
                .To<LevelResultService>()
                .AsSingle();

        private void BindAdsService()
        {
            if (_installData.TargetPlatform == TargetPlatform.YandexGames)
                Container.Bind<IAdsService>()
                    .To<YandexAdsService>()
                    .AsSingle();
            else
                Container.Bind<IAdsService>()
                    .To<NoAdsService>()
                    .AsSingle();
        }

        private void BindLevelStateMachineFactory() =>
            Container.Bind<LevelStateMachine.Factory>()
                .AsSingle();

        private void BindGameStateMachineFactory() =>
            Container.Bind<GameStateMachine.Factory>()
                .AsSingle();
    }
}