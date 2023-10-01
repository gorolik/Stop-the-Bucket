using System;
using System.Collections.Generic;
using Sources.Infrastructure.PersistentProgress;
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
        private IPersistentProgressService _persistentProgress;
        private ILevelsStorageService _levelsStorage;

        private List<LevelButton> _levelButtons = new List<LevelButton>();

        public event Action<int> LevelSelected;

        [Inject]
        private void Construct(IStaticDataService staticData, IPersistentProgressService persistentProgress, ILevelsStorageService levelsStorage)
        {
            _levelsStorage = levelsStorage;
            _staticData = staticData;
            _persistentProgress = persistentProgress;
        }

        private void OnDestroy() => 
            Clear();

        public void Display(ClusterType clusterType)
        {
            Clear();

            DisplayClusterName(clusterType);
            DisplayLevelsList(clusterType);
        }

        private void DisplayClusterName(ClusterType clusterType) => 
            _clusterLabel.text = clusterType.ToString();

        private void OnLevelSelected(int id) =>
            LevelSelected?.Invoke(id);

        private void DisplayLevelsList(ClusterType clusterType)
        {
            foreach (LevelData levelData in _levelsStorage.LevelsData)
            {
                if(levelData.Cluster == clusterType)
                    CreateLevelButton(levelData.Id);
                else
                    break;
            }
        }

        private void CreateLevelButton(int id)
        {
            LevelButton levelButton = Instantiate(_levelButtonPrefab, _levelsContainer);
            
            levelButton.Init(id, 2, true); // ТУТ ПОДГРУЗИТЬ ДАННЫЕ С ПРОГРЕССА ИГРОКА
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