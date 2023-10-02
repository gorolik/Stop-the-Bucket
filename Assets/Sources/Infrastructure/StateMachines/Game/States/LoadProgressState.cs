using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Structure;
using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class LoadProgressState : IState
    {
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IPersistentProgressContainer _progressContainer;
        private readonly IGameStateMachine _gameStateMachine;

        public LoadProgressState(IGameStateMachine gameStateMachine, IPersistentProgressService persistentProgress, IPersistentProgressContainer progressContainer)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgress = persistentProgress;
            _progressContainer = progressContainer;
        }

        public void Enter()
        {
            LoadOrInitProgress();

            _gameStateMachine.Enter<MainMenuState>();
        }

        public void Exit() {}

        private void LoadOrInitProgress() => 
            _progressContainer.PlayerProgress = _persistentProgress.LoadProgress() ?? InitProgress();

        private PlayerProgress InitProgress() => 
            new();
    }
}