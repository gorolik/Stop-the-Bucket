using UnityEngine;

namespace Sources.Behaviour
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        
        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Play() => 
            _source.Play();
    }
}
