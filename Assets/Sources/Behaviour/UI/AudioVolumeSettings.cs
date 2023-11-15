using Sources.Services.AudioMixing;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Behaviour.UI
{
    public class AudioVolumeSettings : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        
        private IAudioMixerService _audioMixer;

        [Inject]
        public void Construct(IAudioMixerService audioMixer) => 
            _audioMixer = audioMixer;

        private void Start()
        {
            _musicSlider.value = _audioMixer.MusicVolume;
            _soundSlider.value = _audioMixer.SoundsVolume;
            
            _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            _soundSlider.onValueChanged.AddListener(OnSoundsSliderValueChanged);
        }
        
        private void OnDestroy()
        {
            _audioMixer.Save();
            
            _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
            _soundSlider.onValueChanged.RemoveListener(OnSoundsSliderValueChanged);
        }

        private void OnMusicSliderValueChanged(float value) => 
            _audioMixer.SetMusicVolume(value);

        private void OnSoundsSliderValueChanged(float value) => 
            _audioMixer.SetSoundsVolume(value);
    }
}
