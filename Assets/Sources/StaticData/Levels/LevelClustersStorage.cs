using System.Collections.Generic;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [CreateAssetMenu(fileName = "LevelClustersStorage", menuName = "Scriptable Objects/Static Data/Level Clusters Storage", order = 51)]
    public class LevelClustersStorage : ScriptableObject
    {
        [SerializeField] private List<LevelClusterData> _levelClusters;
        
        public List<LevelClusterData> LevelClusters => _levelClusters;

        public int GetLevelsCountToCluster(ClusterType type)
        {
            int count = 0;
            
            foreach (LevelClusterData clusterData in _levelClusters)
            {
                count += clusterData.LevelsCount;
                
                if (type == clusterData.Type)
                    break;
            }

            return count;
        }

        public ClusterType GetNextCluster(ClusterType current)
        {
            for (int i = 0; i < _levelClusters.Count; i++)
            {
                if (_levelClusters[i].Type == current && i < _levelClusters.Count - 1)
                    return _levelClusters[i + 1].Type;
            }

            return ClusterType.Amateur;
        }
    }
}