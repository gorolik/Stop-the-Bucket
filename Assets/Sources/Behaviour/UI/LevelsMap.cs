using System;
using System.Collections.Generic;
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
        
        private List<LevelButton> _levelButtons = new List<LevelButton>();

        public event Action<ClusterType, int> LevelSelected;

        [Inject]
        private void Construct(IStaticDataService staticData) => 
            _staticData = staticData;

        private void Start() => 
            Display(ClusterType.Beginner);

        private void OnDestroy() => 
            Clear();

        private void Display(ClusterType clusterType)
        {
            Clear();

            DisplayClusterName(clusterType);
            DisplayLevelsList(clusterType);
        }

        private void DisplayClusterName(ClusterType clusterType) => 
            _clusterLabel.text = clusterType.ToString();

        private void OnLevelSelected(ClusterType cluster, int id) =>
            LevelSelected?.Invoke(cluster, id);

        private void DisplayLevelsList(ClusterType clusterType)
        {
            int levelNumber = 1;
            
            foreach (LevelsCluster cluster in _staticData.GetLevelsData().LevelsClusters)
            {
                if (cluster.ClusterType == clusterType)
                {
                    for (int i = 0; i < cluster.LevelsData.Count; i++) 
                        CreateLevelButton(cluster, i, levelNumber);

                    break;
                }

                levelNumber += cluster.LevelsData.Count;
            }
        }

        private void CreateLevelButton(LevelsCluster cluster, int id, int levelNumber)
        {
            LevelButton levelButton = Instantiate(_levelButtonPrefab, _levelsContainer);
            
            levelButton.Init(cluster.ClusterType, id, levelNumber);
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