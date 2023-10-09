using System;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [Serializable]
    public class LevelClusterData
    {
        [SerializeField] private ClusterType _type;
        [SerializeField] private int _starsToOpen;
        [SerializeField] private int _levelsCount;
        [SerializeField] private ClusterViewData _viewData;

        public ClusterType Type => _type;
        public int StarsToOpen => _starsToOpen;
        public int LevelsCount => _levelsCount;
        public ClusterViewData ViewData => _viewData;
    }
}