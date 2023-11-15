using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Structure;
using Sources.Services.LevelsStorage;
using Sources.Services.Localization;
using Sources.Services.StaticData;
using Sources.StaticData.Levels;
using Sources.UI.Factory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Behaviour.UI.ChooseLevelMenu
{
    public class LevelsMap : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private RectTransform _content;
        [SerializeField] private TMP_Text _clusterLabel;
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private Transform _levelsContainer;
        
        private IStaticDataService _staticData;
        private IPersistentProgressContainer _progressContainer;
        private ILevelsStorageService _levelsStorage;
        private IUIFactory _uiFactory;
        private Localizator _localizator;
        private LevelClustersStorage _clustersStorage;

        private List<LevelButton> _levelButtons = new List<LevelButton>();

        public event Action<int> LevelSelected;
        public event Action<ClusterType> OnClusterDisplayed;

        [Inject]
        private void Construct(IStaticDataService staticData, IPersistentProgressContainer progressContainer,
            ILevelsStorageService levelsStorage, IUIFactory uiFactory, Localizator localizator)
        {
            _localizator = localizator;
            _levelsStorage = levelsStorage;
            _staticData = staticData;
            _progressContainer = progressContainer;
            _uiFactory = uiFactory;
            _clustersStorage = staticData.GetClustersStorage();
        }

        private void OnDestroy() =>
            Clear();

        public void DisplayCluster(ClusterType clusterType)
        {
            Clear();

            CompletedLevel[] completedLevels = _progressContainer.PlayerProgress.CompletedLevels.ToArray();
            int maxCompletedLevelId = -1;

            if (completedLevels.Length > 0)
                maxCompletedLevelId = completedLevels.Max(x => x.Id);

            DisplayClusterView(_clustersStorage.GetDataByType(clusterType).ViewData);
            DisplayLevelsList(clusterType, maxCompletedLevelId);

            _content.transform.position = new Vector2(_content.transform.position.x, 0);
            
            OnClusterDisplayed?.Invoke(clusterType);
        }

        private void DisplayClusterView(ClusterViewData clusterData)
        {
            _clusterLabel.text = _localizator.GetWord(clusterData.NameKey);
            _clusterLabel.color = clusterData.Color;
            
            _background.sprite = clusterData.Background;
        }

        private void OnLevelSelected(int id) =>
            LevelSelected?.Invoke(id);

        private void DisplayLevelsList(ClusterType clusterType, int maxCompletedLevelId)
        {
            LevelData[] levelsData = _levelsStorage.LevelsData.Where(x => x.Cluster == clusterType).ToArray();

            foreach (LevelData levelData in levelsData)
                CreateLevelButton(levelData.Id, maxCompletedLevelId);
        }

        private void CreateLevelButton(int levelId, int maxCompletedLevelId)
        {
            LevelButtonParameters parameters = GetLevelButtonParameters(levelId, maxCompletedLevelId);
            
            LevelButton levelButton = _uiFactory.CreateLevelButton(_levelButtonPrefab, _levelsContainer, parameters);

            levelButton.Clicked += OnLevelSelected;
            _levelButtons.Add(levelButton);
        }

        private LevelButtonParameters GetLevelButtonParameters(int id, int maxCompletedLevelId)
        {
            CompletedLevel[] completedLevels = _progressContainer.PlayerProgress.CompletedLevels.ToArray();
            CompletedLevel completedLevel = completedLevels.FirstOrDefault(x => x.Id == id);

            int stars = 0;
            bool opened = true;

            if (completedLevel != null)
                stars = completedLevel.Stars;

            if (id > maxCompletedLevelId + 1)
                opened = false;

            return new LevelButtonParameters(id, stars, opened);
        }

        private void Clear()
        {
            foreach (LevelButton level in _levelButtons)
            {
                level.Clicked -= OnLevelSelected;
                Destroy(level.gameObject);
            }

            _levelButtons.Clear();
        }
    }
}