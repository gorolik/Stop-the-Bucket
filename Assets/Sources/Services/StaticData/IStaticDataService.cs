using Sources.StaticData.Levels;
using Sources.StaticData.Settings;
using Sources.StaticData.UI;
using Sources.UI;
using UnityEngine.Audio;

namespace Sources.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadData();
        LevelClustersStorage GetClustersStorage();
        GameSettings GetGameSettings();
        WindowConfig GetWindowById(WindowId id);
        StarsSettings GetStarsSettings();
        AudioMixer GetAudioMuxer();
    }
}