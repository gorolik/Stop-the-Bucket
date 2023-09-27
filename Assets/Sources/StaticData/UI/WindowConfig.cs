using System;
using Sources.UI;
using Sources.UI.Windows;
using UnityEngine;

namespace Sources.StaticData.UI
{
    [Serializable]
    public class WindowConfig
    {
        [SerializeField] private WindowId _windowId;
        [SerializeField] private WindowBase _prefab;
        
        public WindowId WindowId => _windowId;
        public WindowBase Prefab => _prefab;
    }
}