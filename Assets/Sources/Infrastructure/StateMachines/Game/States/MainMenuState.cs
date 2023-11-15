using Sources.Behaviour;
using Sources.Behaviour.UI;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.AudioMixing;
using Sources.UI.Factory;
using YG;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class MainMenuState : IState
    {
        private const string _menu = "Menu";

        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressContainer _progressContainer;
        private readonly Curtain _curtain;
        private readonly IProgressListenersContainer _progressListenersContainer;
        private readonly IAudioMixerService _audioMixer;
        private readonly MusicPlayer _musicPlayer;

        private bool _firstOpen = true;

        public MainMenuState(SceneLoader sceneLoader, IUIFactory uiFactory, IGameFactory gameFactory,
            IPersistentProgressContainer progressContainer, Curtain curtain,
            IProgressListenersContainer progressListenersContainer, IAudioMixerService audioMixer, MusicPlayer musicPlayer)
        {
            _curtain = curtain;
            _progressListenersContainer = progressListenersContainer;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _progressContainer = progressContainer;
            _gameFactory = gameFactory;
            _audioMixer = audioMixer;
            _musicPlayer = musicPlayer;
        }

        public void Enter()
        {
            _curtain.Show();
            _sceneLoader.Load(_menu, OnMenuLoaded);
        }

        public void Exit() {}

        private void OnMenuLoaded()
        {
            InitMenu();
            InformProgressReaders();
            
            if(_firstOpen)
                InitAudio();
            
            _curtain.Hide();
            
            TryInitGameReadyAPI();

            _firstOpen = false;
        }

        private void InitMenu()
        {
            _gameFactory.CreateMainMenuHud();
            _uiFactory.CreateUIRoot();

            if (_firstOpen)
                _uiFactory.CreateMainMenu();
            else
                _uiFactory.CreateChooseLevelMenu();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _progressListenersContainer.SavedProgressReaders)
                progressReader.LoadProgress(_progressContainer.PlayerProgress);
        }

        private void InitAudio()
        {
            _audioMixer.Init();
            _musicPlayer.Play();
        }

        private void TryInitGameReadyAPI()
        {
            if (YandexGame.SDKEnabled)
                YandexGame.GameReadyAPI();
        }
    }
}