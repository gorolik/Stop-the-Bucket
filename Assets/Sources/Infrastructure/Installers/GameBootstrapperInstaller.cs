using Sources.Infrastructure.StateMachines;
using Sources.Infrastructure.StateMachines.Game;
using Zenject;

namespace Sources.Infrastructure.Installers
{
    public class GameBootstrapperInstaller : MonoInstaller, IInitializable
    {
        private IStateMachine _stateMachine;

        [Inject]
        public void Construct(
            [Inject (Id = StateMachineType.Game)] IStateMachine stateMachine) =>
            _stateMachine = stateMachine;
        
        public override void InstallBindings() => 
            Container.Bind<IInitializable>().To<GameBootstrapperInstaller>().FromInstance(this);

        public void Initialize() => 
            FindObjectOfType<GameBootstrapper>().Run(_stateMachine);
    }
}