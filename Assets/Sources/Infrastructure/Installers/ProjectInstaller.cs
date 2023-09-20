using Sources.Infrastructure.StateMachines;
using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.Level;
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
            BindGameStateMachine();
            BindLevelStateMachine();
            BindInputService();
        }

        private void BindCoroutineRunner() =>
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

        private void BindSceneLoader() =>
            Container.Bind<SceneLoader>().FromNew().AsSingle();

        private void BindGameStateMachine() => 
            Container.Bind<IStateMachine>().WithId(StateMachineType.Game).To<GameStateMachine>().FromNew().AsSingle();

        private void BindLevelStateMachine() => 
            Container.Bind<IStateMachine>().WithId(StateMachineType.Level).To<LevelStateMachine>().FromNew().AsSingle();

        private void BindInputService()
        {
            Container.Bind<IInputService>().To<DesktopInputService>().FromNew().AsSingle();
        }
    }
}