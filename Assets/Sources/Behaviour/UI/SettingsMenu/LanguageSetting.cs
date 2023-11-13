using System.Linq;
using ModestTree;
using Sources.Services.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Behaviour.UI.SettingsMenu
{
    public class LanguageSetting : MonoBehaviour
    {
        [SerializeField] private LanguageItem[] _languages;
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _label;
        
        private Localizator _localizator;
        private int _currentLanguageId;

        [Inject]
        public void Construct(Localizator localizator) =>
            _localizator = localizator;

        private void Start()
        {
            string lastLanguage = PlayerPrefs.GetString(LocalizationPath.DataKey);
            
            LanguageItem language = _languages.FirstOrDefault(x => x.Value == lastLanguage);
            _currentLanguageId = _languages.IndexOf(language);
            
            SetLanguage(language);
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClicked);
        
        private void OnClicked() => 
            SwitchLanguage();

        private void SwitchLanguage()
        {
            if (_currentLanguageId < _languages.Length - 1)
                _currentLanguageId++;
            else
                _currentLanguageId = 0;

            SetLanguage(_languages[_currentLanguageId]);
        }

        private void SetLanguage(LanguageItem languageItem)
        {
            _localizator.SetLanguage(languageItem.Value);

            _icon.sprite = languageItem.Icon;
            _label.text = languageItem.Name;
        }
    }
}