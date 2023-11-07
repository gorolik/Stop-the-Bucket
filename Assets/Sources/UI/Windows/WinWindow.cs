using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.StateMachines.Game;
using Sources.Infrastructure.StateMachines.Game.States;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Services.LevelsStorage;
using Sources.Services.SceneData;
using Sources.Services.StaticData;
using Sources.StaticData.Levels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.UI.Windows
{
    public class WinWindow : WindowBase
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Image[] _stars;
        
        private ILevelStateMachine _levelStateMachine;
        private IGameStateMachine _gameStateMachine;
        private ISceneDataService _sceneData;
        private ILevelsStorageService _levelsStorage;
        private LevelClustersStorage _clustersStorage;
        private IPersistentProgressContainer _progressContainer;

        [Inject]
        public void Construct(ILevelStateMachine levelStateMachine, IGameStateMachine gameStateMachine, 
            ISceneDataService sceneData, ILevelsStorageService levelsStorage, IStaticDataService staticData,
            IPersistentProgressContainer progressContainer)
        {
            _levelStateMachine = levelStateMachine;
            _gameStateMachine = gameStateMachine;
            _sceneData = sceneData;
            _levelsStorage = levelsStorage;
            _clustersStorage = staticData.GetClustersStorage();
            _progressContainer = progressContainer;
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
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        protected override void Cleanup()
        {
            _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }

        private void OnMenuButtonClicked() => 
            _gameStateMachine.Enter<MainMenuState>();

        private void OnRestartButtonClicked() => 
            _levelStateMachine.Enter<CreateWorldState>();

        private void OnContinueButtonClicked()
        {
            int currentLevelId = _sceneData.LevelData.Id;
            int levelsCountToCluster = _clustersStorage.GetLevelsCountToCluster(_sceneData.LevelData.Cluster);
            
            if (LevelsRemain(currentLevelId) && CurrentClusterRemain(currentLevelId, levelsCountToCluster))
            {
                LevelData nextLevelData = _levelsStorage.LevelsData[currentLevelId + 1];
                
                _sceneData.Init(nextLevelData);
                _levelStateMachine.Enter<CreateWorldState>();
            }
            else
            {
                if (!CurrentClusterRemain(currentLevelId, levelsCountToCluster))
                    _progressContainer.PlayerProgress.SelectedCluster = _clustersStorage.GetNextCluster(_sceneData.LevelData.Cluster);
                
                _gameStateMachine.Enter<MainMenuState>();
            }
        }

        private bool LevelsRemain(int currentLevelId) => 
            currentLevelId + 1 < _levelsStorage.LevelsData.Count;

        private bool CurrentClusterRemain(int currentLevelId, int levelsCountToCluster) => 
            currentLevelId + 1 < levelsCountToCluster;
    }
}