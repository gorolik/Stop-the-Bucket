using System;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [Serializable]
    public class LevelClusterData
    {
        [Header("Levels Settings")] 
        [SerializeField] [Range(0.3f, 2.6f)] private float _lineRange;
        [SerializeField] [Range(10f, 80f)] private float _startMaxSpeed;
        [SerializeField] [Range(10f, 80f)] private float _endMaxSpeed;
        [SerializeField] [Range(2f, 32f)] private float _startAcceleration;
        [SerializeField] [Range(2f, 32f)] private float _endAcceleration;
        [Header("Cluster Settings")]
        [SerializeField] private ClusterType _type;
        [SerializeField] private int _starsToOpen;
        [SerializeField] private int _levelsCount;
        [Header("View Settings")]
        [SerializeField] private ClusterViewData _viewData;

        public float LineRange => _lineRange;
        public float StartMaxSpeed => _startMaxSpeed;
        public float EndMaxSpeed => _endMaxSpeed;
        public float StartAcceleration => _startAcceleration;
        public float EndAcceleration => _endAcceleration;
        public ClusterType Type => _type;
        public int StarsToOpen => _starsToOpen;
        public int LevelsCount => _levelsCount;
        public ClusterViewData ViewData => _viewData;
    }
}