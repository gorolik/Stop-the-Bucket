using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Infrastructure.StateMachines.States;
using Zenject;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class LevelLoopState : IState
    {
        private readonly IStateMachine _levelStateMachine;

        public LevelLoopState(
            [Inject(Id = StateMachineType.Level)] IStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }
        
        public void Enter()
        {
            _levelStateMachine.Enter<CountingState>();
        }

        public void Exit()
        {
            
        }
    }
}