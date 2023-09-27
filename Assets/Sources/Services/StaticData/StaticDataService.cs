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
        private LevelsData _levelsData;
        private GameSettings _gameSettings;
        private Dictionary<WindowId, WindowConfig> _windows;

        public void LoadData()
        {
            LoadLevelsData();
            LoadGameSettings();
            LoadWindowsData();
        }

        public LevelsData GetLevelsData() =>
            _levelsData;

        public GameSettings GetGameSettings() => 
            _gameSettings;

        public WindowConfig GetWindowById(WindowId id) => 
            _windows.TryGetValue(id, out WindowConfig data) ? data : null;

        private void LoadLevelsData() => 
            _levelsData = Resources.Load<LevelsData>("StaticData/Levels/LevelsData");

        private void LoadGameSettings() => 
            _gameSettings = Resources.Load<GameSettings>("StaticData/Settings/GameSettings");
        
        private void LoadWindowsData()
        {
            _windows = Resources
                .Load<WindowsStaticData>("StaticData/Windows/WindowsData")
                .Windows
                .ToDictionary(x => x.WindowId, x => x);
        }
    }
}