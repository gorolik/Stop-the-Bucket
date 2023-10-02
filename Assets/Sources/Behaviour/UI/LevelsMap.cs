using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Structure;
using Sources.Services.LevelsStorage;
using Sources.Services.StaticData;
using Sources.StaticData.Levels;
using TMPro;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.UI
{
    public class LevelsMap : MonoBehaviour
    {
        [SerializeField] private TMP_Text _clusterLabel;
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private Transform _levelsContainer;

        private IStaticDataService _staticData;
        private IPersistentProgressContainer _progressContainer;
        private ILevelsStorageService _levelsStorage;

        private List<LevelButton> _levelButtons = new List<LevelButton>();

        public event Action<int> LevelSelected;

        [Inject]
        private void Construct(IStaticDataService staticData, IPersistentProgressContainer progressContainer, ILevelsStorageService levelsStorage)
        {
            _levelsStorage = levelsStorage;
            _staticData = staticData;
            _progressContainer = progressContainer;
        }

        private void OnDestroy() => 
            Clear();

        public void DisplayCluster(ClusterType clusterType)
        {
            Clear();
            
            CompletedLevel[] completedLevels = _progressContainer.PlayerProgress.CompletedLevels.ToArray();
            int maxCompletedLevelId = 0;
            
            Debug.Log(completedLevels.Length);
            
            if(completedLevels.Length > 0)
                maxCompletedLevelId = completedLevels.Max(x=>x.Id);
            
            DisplayClusterName(clusterType);
            DisplayLevelsList(clusterType, maxCompletedLevelId);
        }

        private void DisplayClusterName(ClusterType clusterType) => 
            _clusterLabel.text = clusterType.ToString();

        private void OnLevelSelected(int id) =>
            LevelSelected?.Invoke(id);

        private void DisplayLevelsList(ClusterType clusterType, int maxCompletedLevelId)
        {
            foreach (LevelData levelData in _levelsStorage.LevelsData)
            {
                if(levelData.Cluster == clusterType)
                    CreateLevelButton(levelData.Id, maxCompletedLevelId);
                else
                    break;
            }
        }

        private void CreateLevelButton(int id, int maxCompletedLevelId)
        {
            CompletedLevel[] completedLevels = _progressContainer.PlayerProgress.CompletedLevels.ToArray();
            CompletedLevel completedLevel = completedLevels.FirstOrDefault(x => x.Id == id);

            int stars = 0;
            bool opened = true;
            
            if (completedLevel != null)
                stars = completedLevel.Stars;
            
            if (id + 1 > maxCompletedLevelId)
                opened = false;
            
            LevelButton levelButton = Instantiate(_levelButtonPrefab, _levelsContainer);
            
            levelButton.Init(id, stars, opened);
            levelButton.Clicked += OnLevelSelected;
            
            _levelButtons.Add(levelButton);
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