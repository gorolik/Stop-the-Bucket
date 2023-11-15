using UnityEngine;

namespace Sources.StaticData.Peoples
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Static Data/People", fileName = "PeopleData", order = 51)]
    public class PeopleData : ScriptableObject
    {
        [SerializeField] private Sprite _waitEmotion;
        [SerializeField] private Sprite _winEmotion;
        [SerializeField] private Sprite _loseEmotion;

        public Sprite WaitEmotion => _waitEmotion;
        public Sprite WinEmotion => _winEmotion;
        public Sprite LoseEmotion => _loseEmotion;
    }
}