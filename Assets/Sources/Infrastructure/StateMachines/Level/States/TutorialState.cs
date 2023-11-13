using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.StateMachines.States;
using Sources.UI.Factory;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class TutorialState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressContainer _progressContainer;

        public TutorialState(IUIFactory uiFactory, IPersistentProgressContainer progressContainer)
        {
            _uiFactory = uiFactory;
            _progressContainer = progressContainer;
        }

        public void Enter() => 
            _uiFactory.CreateTutorialWindow();

        public void Exit() => 
            _progressContainer.PlayerProgress.TutorialComplete = true;
    }
}