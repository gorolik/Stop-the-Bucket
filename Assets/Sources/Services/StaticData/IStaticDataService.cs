using Sources.StaticData.Levels;
using Sources.StaticData.Settings;
using Sources.StaticData.UI;
using Sources.UI;

namespace Sources.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadData();
        LevelsSettingsStorage GetLevelsSettingsStorage();
        LevelClustersStorage GetClustersStorage();
        GameSettings GetGameSettings();
        WindowConfig GetWindowById(WindowId id);
        StarsSettings GetStarsSettings();
    }
}