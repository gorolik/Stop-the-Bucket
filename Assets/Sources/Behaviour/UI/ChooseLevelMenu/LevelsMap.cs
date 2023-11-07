using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Structure;
using Sources.Services.LevelsStorage;
using Sources.Services.StaticData;
using Sources.StaticData.Levels;
using Sources.UI.Factory;
using TMPro;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.UI.ChooseLevelMenu
{
    public class LevelsMap : MonoBehaviour
    {
        [SerializeField] private TMP_Text _clusterLabel;
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private Transform _levelsContainer;
        
        private IStaticDataService _staticData;
        private IPersistentProgressContainer _progressContainer;
        private ILevelsStorageService _levelsStorage;
        private IUIFactory _uiFactory;
        
        private List<LevelButton> _levelButtons = new List<LevelButton>();

        public event Action<int> LevelSelected;
        public event Action<ClusterType> OnClusterDisplayed;

        [Inject]
        private void Construct(IStaticDataService staticData, IPersistentProgressContainer progressContainer,
            ILevelsStorageService levelsStorage, IUIFactory uiFactory)
        {
            _levelsStorage = levelsStorage;
            _staticData = staticData;
            _progressContainer = progressContainer;
            _uiFactory = uiFactory;
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

            DisplayClusterName(clusterType);
            DisplayLevelsList(clusterType, maxCompletedLevelId);
            
            OnClusterDisplayed?.Invoke(clusterType);
        }

        private void DisplayClusterName(ClusterType clusterType) =>
            _clusterLabel.text = clusterType.ToString();

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