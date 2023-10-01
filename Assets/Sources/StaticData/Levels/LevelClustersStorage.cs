using System.Collections.Generic;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [CreateAssetMenu(fileName = "LevelClustersStorage", menuName = "Scriptable Objects/Static Data/Level Clusters Storage", order = 51)]
    public class LevelClustersStorage : ScriptableObject
    {
        [SerializeField] private List<LevelClusterData> _levelClusters;
        
        public List<LevelClusterData> LevelClusters => _levelClusters;
    }
}