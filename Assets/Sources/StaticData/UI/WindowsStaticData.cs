using System.Collections.Generic;
using UnityEngine;

namespace Sources.StaticData.UI
{
    [CreateAssetMenu(fileName = "WindowsData", menuName = "Scriptable Objects/Static Data/Windows Data")]
    public class WindowsStaticData : ScriptableObject
    {
        [SerializeField] private List<WindowConfig> _windows;
        
        public IEnumerable<WindowConfig> Windows => _windows;
    }
}