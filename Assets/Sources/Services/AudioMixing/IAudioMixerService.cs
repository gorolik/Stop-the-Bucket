namespace Sources.Services.AudioMixing
{
    public interface IAudioMixerService
    {
        void Init();
        void Save();
        void SetMusicVolume(float value);
        void SetSoundsVolume(float value);
        float MusicVolume { get; }
        float SoundsVolume { get; }
    }
}