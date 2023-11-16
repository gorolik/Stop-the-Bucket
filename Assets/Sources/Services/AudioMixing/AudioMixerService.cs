using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.PersistentProgress.Services;
using Sources.Services.StaticData;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Services.AudioMixing
{
    public class AudioMixerService : IAudioMixerService
    {
        private const string _musicVolumeParameter = "MusicVolume";
        private const string _soundsVolumeParameter = "SoundsVolume";

        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressContainer _progressContainer;
        private readonly IPersistentProgressService _persistentProgress;

        private AudioMixer _mixer;

        public float MusicVolume => _progressContainer.PlayerProgress.MusicVolume;
        public float SoundsVolume => _progressContainer.PlayerProgress.SoundsVolume;
        
        public AudioMixerService(IStaticDataService staticData, IPersistentProgressContainer progressContainer, IPersistentProgressService persistentProgress)
        {
            _staticData = staticData;
            _progressContainer = progressContainer;
            _persistentProgress = persistentProgress;
        }

        public void Init()
        {
            _mixer = _staticData.GetAudioMuxer();
            
            SetMusicVolume(_progressContainer.PlayerProgress.MusicVolume);
            SetSoundsVolume(_progressContainer.PlayerProgress.SoundsVolume);
        }

        public void Save() => 
            _persistentProgress.SaveProgress();

        public void SetMusicVolume(float value)
        {
            _mixer.SetFloat(_musicVolumeParameter, GetUnityMixerValueBy01(value));
            _progressContainer.PlayerProgress.MusicVolume = value;
        }

        public void SetSoundsVolume(float value)
        {
            _mixer.SetFloat(_soundsVolumeParameter, GetUnityMixerValueBy01(value));
            _progressContainer.PlayerProgress.SoundsVolume = value;
        }

        private float GetUnityMixerValueBy01(float value) => 
            Mathf.Clamp(Mathf.Log10(value) * 20, -80, 0);
    }
}