using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [Serializable]
    public class LevelsCluster
    {
        [SerializeField] private ClusterType _clusterType;
        [SerializeField] private List<LevelData> _levelsData;
        
        public ClusterType ClusterType => _clusterType;
        public IReadOnlyList<LevelData> LevelsData => _levelsData;
    }
}