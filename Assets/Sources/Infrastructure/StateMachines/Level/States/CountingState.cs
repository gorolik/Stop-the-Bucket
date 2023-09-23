using Sources.Infrastructure.StateMachines.States;
using Sources.Services.Timer;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class CountingState : IState
    {
        private const float _timeToStart = 3;
        
        private readonly ILevelStateMachine _levelStateMachine;
        private readonly ITimersHandler _timersHandler;

        private TimersHandler.Timer _currentTimer;

        public CountingState(ILevelStateMachine levelStateMachine, ITimersHandler timersHandler)
        {
            _levelStateMachine = levelStateMachine;
            _timersHandler = timersHandler;
        }
        
        public void Enter()
        {
            _currentTimer = _timersHandler.StartNewTimer(_timeToStart);
            _currentTimer.TimeEnd += OnTimeEnd;
        }

        public void Exit()
        {
            _currentTimer.TimeEnd -= OnTimeEnd;
        }

        private void OnTimeEnd(TimersHandler.Timer timer) => 
            _levelStateMachine.Enter<CatchingState>();
    }
}