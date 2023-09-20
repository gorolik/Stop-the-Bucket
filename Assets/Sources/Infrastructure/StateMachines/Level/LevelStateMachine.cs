using System;
using System.Collections.Generic;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.Level
{
    public class LevelStateMachine : StateMachine, ILevelStateMachine
    {
        public LevelStateMachine()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(CountingState)] = new CountingState(this),
                [typeof(CatchingState)] = new CatchingState(),
                [typeof(WinState)] = new WinState(),
                [typeof(LoseState)] = new LoseState(),
            };
        }
    }
}