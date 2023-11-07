using Sources.Infrastructure.StateMachines.States;
using Sources.Services.Timer;
using Sources.UI.Factory;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class CountingState : IState
    {
        private const float _timeToStart = 2;
        
        private readonly ILevelStateMachine _levelStateMachine;
        private readonly ITimersHandler _timersHandler;
        private readonly IUIFactory _uiFactory;

        private TimersHandler.Timer _currentTimer;

        public CountingState(ILevelStateMachine levelStateMachine, ITimersHandler timersHandler, IUIFactory uiFactory)
        {
            _levelStateMachine = levelStateMachine;
            _timersHandler = timersHandler;
            _uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            _currentTimer = _timersHandler.StartNewTimer(_timeToStart);
            _currentTimer.TimeEnd += OnTimeEnd;

            _uiFactory.CreateCountingWindow(_currentTimer);
        }

        public void Exit() => 
            _currentTimer.TimeEnd -= OnTimeEnd;

        private void OnTimeEnd(TimersHandler.Timer timer) => 
            _levelStateMachine.Enter<CatchingState>();
    }
}