using Sources.Infrastructure.StateMachines.States;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class LoseState : IState
    {
        public void Enter()
        {
            Debug.Log("Lose");
        }

        public void Exit() {}
    }
}