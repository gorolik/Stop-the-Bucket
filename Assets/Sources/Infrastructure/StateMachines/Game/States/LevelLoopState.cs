using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Infrastructure.StateMachines.States;
using Sources.StaticData.Levels;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class LevelLoopState : IPayloadState<LevelData>
    {
        private readonly LevelStateMachine.Factory _levelStateMachineFactory;
        
        private ILevelStateMachine _levelStateMachine;

        public LevelLoopState(LevelStateMachine.Factory levelStateMachineFactory) => 
            _levelStateMachineFactory = levelStateMachineFactory;

        public void Enter(LevelData levelData)
        {
            _levelStateMachine = _levelStateMachineFactory.Create();
            _levelStateMachine.Enter<CreateWorldState, LevelData>(levelData);
        }

        public void Exit() {}
    }
}