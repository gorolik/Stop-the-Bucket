using Sources.StaticData.Levels;
using Sources.StaticData.Settings;
using Sources.StaticData.UI;
using Sources.UI;

namespace Sources.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadData();
        LevelsData GetLevelsData();
        GameSettings GetGameSettings();
        WindowConfig GetWindowById(WindowId id);
    }
}