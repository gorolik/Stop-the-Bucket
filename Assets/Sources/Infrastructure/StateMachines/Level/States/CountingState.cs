using Sources.Infrastructure.StateMachines.States;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class CountingState : IState
    {
        private readonly IStateMachine _levelStateMachine;

        public CountingState(IStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }
        
        public void Enter()
        {
            Debug.Log("Game Soon Begin...");
            _levelStateMachine.Enter<CatchingState>();
        }

        public void Exit()
        {
            
        }
    }
}