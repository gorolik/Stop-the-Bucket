using Sources.Services;
using Zenject;

namespace Sources.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindCoroutineRunner();
            BindSceneLoader();
            BindInputService();
        }

        private void BindCoroutineRunner() =>
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

        private void BindSceneLoader() =>
            Container.Bind<SceneLoader>().FromNew().AsSingle();

        private void BindInputService()
        {
            Container.Bind<IInputService>().To<DesktopInputService>().FromNew().AsSingle();
        }
    }
}