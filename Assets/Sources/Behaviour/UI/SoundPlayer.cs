using UnityEngine;

namespace Sources.Behaviour.UI
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioClip sound, float volume)
        {   
            float sourceVolume = _audioSource.volume;

            _audioSource.volume = volume;
            _audioSource.PlayOneShot(sound);
            _audioSource.volume = sourceVolume;
        }
    }
}