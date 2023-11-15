using System.Linq;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.StaticData;
using Sources.StaticData.Levels;
using TMPro;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.UI.ChooseLevelMenu
{
    public class ClusterStateDisplayer : MonoBehaviour
    {
        [SerializeField] private LevelsMap _map;
        [SerializeField] private GameObject _closeObject;
        [SerializeField] private TMP_Text _starsToOpenDisplay;

        private IStaticDataService _staticData;
        private IPersistentProgressContainer _progressContainer;

        [Inject]
        public void Construct(IStaticDataService staticData, IPersistentProgressContainer progressContainer)
        {
            _staticData = staticData;
            _progressContainer = progressContainer;
        }
        
        private void OnEnable() => 
            _map.OnClusterDisplayed += OnClusterDisplayer;
        
        private void OnDisable() => 
            _map.OnClusterDisplayed -= OnClusterDisplayer;

        private void OnClusterDisplayer(ClusterType type)
        {
            LevelClustersStorage clustersStorage = _staticData.GetClustersStorage();
            LevelClusterData currentCluster = clustersStorage.GetDataByType(type);

            int playerStars = _progressContainer.PlayerProgress.GetPlayerStarsCount();

            _starsToOpenDisplay.text = playerStars + "/" + currentCluster.StarsToOpen;
            
            if (playerStars >= currentCluster.StarsToOpen)
                _closeObject.SetActive(false);
            else
                _closeObject.SetActive(true);
        }
    }
}