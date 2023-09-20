using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class LevelLoopState : IState
    {
        private readonly ILevelStateMachine _levelStateMachine;

        public LevelLoopState()
        {
            _levelStateMachine = new LevelStateMachine();
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