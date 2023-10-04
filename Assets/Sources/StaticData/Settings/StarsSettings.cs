using UnityEngine;

namespace Sources.StaticData.Settings
{
    [CreateAssetMenu(fileName = "StarsSettings", menuName = "Scriptable Objects/Stars Settings", order = 51)]
    public class StarsSettings : ScriptableObject
    {
        [SerializeField] private int[] _closePercents = new int[3];

        public int[] ClosePercents => _closePercents;
    }
}