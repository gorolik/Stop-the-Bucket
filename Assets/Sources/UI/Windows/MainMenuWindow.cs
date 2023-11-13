using Sources.UI.Factory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.UI.Windows
{
    public class MainMenuWindow : WindowBase
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;

        private IUIFactory _uiFactory;

        [Inject]
        public void Construct(IUIFactory uiFactory) =>
            _uiFactory = uiFactory;
        
        protected override void SubscribeUpdates()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        }

        protected override void Cleanup()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _uiFactory.CreateChooseLevelMenu();
            Close();
        }

        private void OnSettingsButtonClicked()
        {
            _uiFactory.CreateSettingsWindow();
            Close();
        }
    }
}