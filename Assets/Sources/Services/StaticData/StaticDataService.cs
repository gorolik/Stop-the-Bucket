using System.Collections.Generic;
using System.Linq;
using Sources.StaticData.Levels;
using Sources.StaticData.Settings;
using Sources.StaticData.UI;
using Sources.UI;
using UnityEngine;

namespace Sources.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private LevelsSettingsStorage _levelsSettingsStorage;
        private LevelClustersStorage _levelClustersStorage;
        private GameSettings _gameSettings;
        private StarsSettings _starsSettings;
        private Dictionary<WindowId, WindowConfig> _windows;

        public void LoadData()
        {
            LoadLevelsSettingsStorage();
            LoadLevelClustersStorage();
            LoadGameSettings();
            LoadWindowsData();
            LoadStarsSettings();
        }

        public LevelsSettingsStorage GetLevelsSettingsStorage() =>
            _levelsSettingsStorage;

        public LevelClustersStorage GetClustersStorage() => 
            _levelClustersStorage;

        public GameSettings GetGameSettings() => 
            _gameSettings;

        public WindowConfig GetWindowById(WindowId id) => 
            _windows.TryGetValue(id, out WindowConfig data) ? data : null;

        public StarsSettings GetStarsSettings() => 
            _starsSettings;

        private void LoadLevelsSettingsStorage() => 
            _levelsSettingsStorage = Resources.Load<LevelsSettingsStorage>("StaticData/Levels/LevelsSettingsStorage");

        private void LoadLevelClustersStorage() => 
            _levelClustersStorage = Resources.Load<LevelClustersStorage>("StaticData/Levels/LevelClustersStorage");

        private void LoadGameSettings() => 
            _gameSettings = Resources.Load<GameSettings>("StaticData/Settings/GameSettings");
        
        private void LoadWindowsData()
        {
            _windows = Resources
                .Load<WindowsStaticData>("StaticData/Windows/WindowsData")
                .Windows
                .ToDictionary(x => x.WindowId, x => x);
        }

        private void LoadStarsSettings() => 
            _starsSettings = Resources.Load<StarsSettings>("StaticData/Settings/StarsSettings");
    }
}