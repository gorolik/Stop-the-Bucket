using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.Game.States;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Services.LevelsStorage;
using Sources.Services.SceneData;
using Sources.StaticData.Levels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.UI.Windows
{
    public class WinWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Image[] _stars;
        
        private ILevelStateMachine _levelStateMachine;
        private IGameStateMachine _gameStateMachine;
        private ISceneDataService _sceneData;
        private ILevelsStorageService _levelsStorage;

        [Inject]
        public void Construct(ILevelStateMachine levelStateMachine, IGameStateMachine gameStateMachine, ISceneDataService sceneData, ILevelsStorageService levelsStorage)
        {
            _levelStateMachine = levelStateMachine;
            _gameStateMachine = gameStateMachine;
            _sceneData = sceneData;
            _levelsStorage = levelsStorage;
        }

        public void Init(int starsCount)
        {
            for (int i = 0; i < _stars.Length; i++)
            {
                if (starsCount > 0)
                    _stars[i].color = Color.yellow;
                else
                    _stars[i].color = Color.gray;

                starsCount -= 1;
            }
        }
        
        protected override void SubscribeUpdates()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        protected override void Cleanup()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }

        private void OnRestartButtonClicked() => 
            _levelStateMachine.Enter<CreateWorldState>();

        private void OnContinueButtonClicked()
        {
            int currentLevelId = _sceneData.LevelData.Id;
            
            if (currentLevelId + 1 < _levelsStorage.LevelsData.Count)
            {
                LevelData nextLevelData = _levelsStorage.LevelsData[currentLevelId + 1];
                
                _sceneData.Init(nextLevelData);
                _levelStateMachine.Enter<CreateWorldState>();
            }
            else
            {
                _gameStateMachine.Enter<MainMenuState>();
            }
        }
    }
}