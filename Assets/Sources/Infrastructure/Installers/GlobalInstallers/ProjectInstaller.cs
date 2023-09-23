using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Services;
using Zenject;

namespace Sources.Infrastructure.Installers.GlobalInstallers
{
    public class ProjectInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindCoroutineRunner();
            BindSceneLoader();
            BindInputService();
            BindAssetProvider();
            BindGameFactory();
            BindLevelStateMachineFactory();
            BindGameStateMachineFactory();
        }

        private void BindCoroutineRunner() =>
            Container.Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle();

        private void BindSceneLoader() =>
            Container.Bind<SceneLoader>()
                .AsSingle();

        private void BindInputService()
        {
            Container.Bind<IInputService>()
                .To<DesktopInputService>()
                .AsSingle();
        }

        private void BindAssetProvider() =>
            Container.Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();

        private void BindGameFactory()
        {
            Container.Bind<IGameFactory>()
                .To<GameFactory>()
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