namespace Sources.Infrastructure.StateMachines.States
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}
