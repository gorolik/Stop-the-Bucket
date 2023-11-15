using System;
using Sources.StaticData.Peoples;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [Serializable]
    public class ClusterViewData
    {
        [SerializeField] private string _nameKey;
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private Sprite _background;
        [SerializeField] private PeopleData _people;

        public string NameKey => _nameKey;
        public Color Color => _color;
        public Sprite Background => _background;
        public PeopleData People => _people;
    }
}