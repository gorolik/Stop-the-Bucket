using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
    }
}