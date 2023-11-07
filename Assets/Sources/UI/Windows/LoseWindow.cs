using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.Game.States;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.UI.Windows
{
    public class LoseWindow : WindowBase
    {
        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _loseSound;
        
        [Header("UI")]
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartButton;
        
        private IGameStateMachine _gameStateMachine;
        private ILevelStateMachine _levelStateMachine;

        [Inject]
        public void Construct(ILevelStateMachine levelStateMachine, IGameStateMachine gameStateMachine)
        {
            _levelStateMachine = levelStateMachine;
            _gameStateMachine = gameStateMachine;
        }

        protected override void OnStart() => 
            _audioSource.PlayOneShot(_loseSound);

        protected override void SubscribeUpdates()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        protected override void Cleanup()
        {
            _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnMenuButtonClicked() => 
            _gameStateMachine.Enter<MainMenuState>();

        private void OnRestartButtonClicked() => 
            _levelStateMachine.Enter<CreateWorldState>();
    }
}
