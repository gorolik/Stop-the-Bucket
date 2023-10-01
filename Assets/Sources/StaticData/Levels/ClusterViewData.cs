using System;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [Serializable]
    public class ClusterViewData
    {
        public Sprite Background => _background;
        public Sprite People => _people;

        [SerializeField] private Sprite _background;
        [SerializeField] private Sprite _people;
    }
}