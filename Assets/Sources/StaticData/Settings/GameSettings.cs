using UnityEngine;

namespace Sources.StaticData.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/Game Settings", order = 51)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] [Range(1, 2.8f)] private float _bucketHeight = 2.5f;
        [SerializeField] [Range(-3, -1)] private float _peopleHeight = -1.5f;
        
        public float BucketHeight => _bucketHeight;
        public float PeopleHeight => _peopleHeight;
    }
}