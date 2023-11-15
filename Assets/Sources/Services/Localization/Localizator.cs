using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Services.Localization
{
    public class Localizator
    {
        private readonly LocalizationLoader _localizationLoader;
        private readonly LocalizationStorage _localizationStorage;

        public event Action LanguageChanged;

        public bool Ready => _localizationStorage.LocalizedText != null;
        
        private List<string> _languages = new List<string>()
        {
            "ru_RU",
            "en_US"
        };

        public Localizator(LocalizationLoader localizationLoader, LocalizationStorage localizationStorage)
        {
            _localizationLoader = localizationLoader;
            _localizationStorage = localizationStorage;
        }

        public void Init()
        {
            if (PlayerPrefs.HasKey(LocalizationPath.DataKey) == false)
            {
                if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
                    SetLanguage(_languages[0]);
                else
                    SetLanguage(_languages[1]);
            }
            else
            {
                SetLanguage(PlayerPrefs.GetString(LocalizationPath.DataKey));
            }
        }

        public string GetWord(string key)
        {
            if (_localizationStorage.LocalizedText.TryGetValue(key, out var word))
                return word;
            else
            {
                Debug.LogError("Key: " + key + " not founded in dictionary");
                return key;
            }
        }

        public void SetLanguage(string language)
        {
            if (_languages.Contains(language))
                _localizationLoader.Load(language, LanguageChanged);
            else
                Debug.LogError("Unknown language, check languages store");
        }
    }
}