using Sources.Infrastructure.StateMachines.States;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class CatchingState : IState
    {
        public void Enter()
        {
            Debug.Log("Game Started!");
        }

        public void Exit() {}
    }
}