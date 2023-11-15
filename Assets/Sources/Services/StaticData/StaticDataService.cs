using System.Collections.Generic;
using System.Linq;
using Sources.StaticData.Levels;
using Sources.StaticData.Settings;
using Sources.StaticData.UI;
using Sources.UI;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private LevelClustersStorage _levelClustersStorage;
        private GameSettings _gameSettings;
        private StarsSettings _starsSettings;
        private Dictionary<WindowId, WindowConfig> _windows;
        private AudioMixer _audioMixer;

        public void LoadData()
        {
            LoadLevelClustersStorage();
            LoadGameSettings();
            LoadWindowsData();
            LoadStarsSettings();
            LoadAudioMixer();
        }

        public LevelClustersStorage GetClustersStorage() => 
            _levelClustersStorage;

        public GameSettings GetGameSettings() => 
            _gameSettings;

        public WindowConfig GetWindowById(WindowId id) => 
            _windows.TryGetValue(id, out WindowConfig data) ? data : null;

        public StarsSettings GetStarsSettings() => 
            _starsSettings;

        public AudioMixer GetAudioMuxer() => 
            _audioMixer;

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

        private void LoadAudioMixer() => 
            _audioMixer = Resources.Load<AudioMixer>("AudioMixers/MainMixer");
    }
}