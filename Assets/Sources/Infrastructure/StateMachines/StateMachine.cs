using System;
using System.Collections.Generic;
using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.States;
using Zenject;

namespace Sources.Infrastructure.StateMachines
{
    public abstract class StateMachine : IStateMachine
    {
        protected Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            IPayloadState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}