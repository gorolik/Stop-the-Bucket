using System.Collections.Generic;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [CreateAssetMenu(fileName = "LevelsSettingsStorage", menuName = "Scriptable Objects/Static Data/Levels Settings Storage", order = 51)]
    public class LevelsSettingsStorage : ScriptableObject
    {
        [SerializeField] private List<LevelSettings> _levelsSettings;

        public IReadOnlyList<LevelSettings> LevelsSettings => _levelsSettings;
    }
}