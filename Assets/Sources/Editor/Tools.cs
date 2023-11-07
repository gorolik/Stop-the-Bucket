using UnityEditor;
using UnityEngine;

namespace Sources.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear Prefs Data")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
