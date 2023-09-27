using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Scriptable Objects/Static Data/Levels Data", order = 51)]
    public class LevelsData : ScriptableObject
    {
        [SerializeField] private List<LevelsCluster> _levelsClusters;

        public IReadOnlyList<LevelsCluster> LevelsClusters => _levelsClusters;
    }
}