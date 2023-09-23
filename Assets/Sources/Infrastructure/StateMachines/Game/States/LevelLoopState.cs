using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class LevelLoopState : IState
    {
        private readonly LevelStateMachine.Factory _levelStateMachineFactory;
        
        private ILevelStateMachine _levelStateMachine;

        public LevelLoopState(LevelStateMachine.Factory levelStateMachineFactory) => 
            _levelStateMachineFactory = levelStateMachineFactory;

        public void Enter()
        {
            _levelStateMachine = _levelStateMachineFactory.Create();
            _levelStateMachine.Enter<CreateWorldState>();
        }

        public void Exit() {}
    }
}