using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Behaviour.UI.SettingsMenu
{
    [Serializable]
    public class LanguageItem
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private string _value;

        public Sprite Icon => _icon;
        public string Name => _name;
        public string Value => _value;
    }
}