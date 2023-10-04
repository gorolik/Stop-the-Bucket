using System;
using Sources.Services.StaticData;
using Sources.StaticData.Levels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Behaviour.UI.ChooseLevelMenu
{
    public class MapClusterSwitcher : MonoBehaviour
    {
        [SerializeField] private LevelsMap _map;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        
        private LevelClustersStorage _clustersStorage;

        private int _currentClusterId;

        [Inject]
        public void Construct(IStaticDataService staticData) => 
            _clustersStorage = staticData.GetClustersStorage();

        private void OnEnable()
        {
            _map.OnClusterDisplayed += OnClusterSwitched;
            _rightArrow.onClick.AddListener(RightArrowButtonClicked);
            _leftArrow.onClick.AddListener(LeftArrowButtonClicked);
        }

        private void OnDisable()
        {
            _rightArrow.onClick.RemoveListener(RightArrowButtonClicked);
            _leftArrow.onClick.RemoveListener(LeftArrowButtonClicked);
        }

        private void RightArrowButtonClicked() => 
            _map.DisplayCluster(GetClusterTypeById(_currentClusterId + 1));

        private void LeftArrowButtonClicked() => 
            _map.DisplayCluster(GetClusterTypeById(_currentClusterId - 1));

        private void OnClusterSwitched(ClusterType cluster)
        {
            LevelClusterData clusterData = _clustersStorage.LevelClusters.Find(x => x.Type == cluster);
            _currentClusterId = _clustersStorage.LevelClusters.IndexOf(clusterData);
            Debug.Log(_currentClusterId);
            UpdateArrows();
        }

        private void UpdateArrows()
        {
            _leftArrow.gameObject.SetActive(_currentClusterId != 0);
            _rightArrow.gameObject.SetActive(_currentClusterId != _clustersStorage.LevelClusters.Count - 1);
        }

        private ClusterType GetClusterTypeById(int id)
        {
            id = Mathf.Clamp(id, 0, _clustersStorage.LevelClusters.Count - 1);
            return _clustersStorage.LevelClusters[id].Type;
        }
    }
}