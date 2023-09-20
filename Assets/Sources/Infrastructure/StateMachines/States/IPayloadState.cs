namespace Sources.Infrastructure.StateMachines.States
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}
