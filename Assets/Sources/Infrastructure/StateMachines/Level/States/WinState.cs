using Sources.Infrastructure.StateMachines.States;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class WinState : IState
    {
        public void Enter()
        {
            Debug.Log("Win");
        }

        public void Exit() {}
    }
}