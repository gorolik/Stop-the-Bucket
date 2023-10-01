using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Infrastructure.StateMachines.States;
using Sources.UI.Factory;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class LevelLoopState : IState
    {
        private readonly LevelStateMachine.Factory _levelStateMachineFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        private ILevelStateMachine _levelStateMachine;

        public LevelLoopState(LevelStateMachine.Factory levelStateMachineFactory, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _levelStateMachineFactory = levelStateMachineFactory;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _levelStateMachine = _levelStateMachineFactory.Create();
            _levelStateMachine.Enter<CreateWorldState>();
        }

        public void Exit()
        {
            _gameFactory.Cleanup();
            _uiFactory.Cleanup();
            _levelStateMachineFactory.Cleanup();
        }
    }
}