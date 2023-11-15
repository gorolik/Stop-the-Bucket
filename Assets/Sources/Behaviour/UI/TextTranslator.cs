using Sources.Services.Localization;
using TMPro;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextTranslator : MonoBehaviour
    {
        [SerializeField] private string _key;

        private Localizator _localizator;
        private TMP_Text _text;

        [Inject]
        public void Construct(Localizator localizator)
        {
            _localizator = localizator;

            _localizator.LanguageChanged += OnLanguageChanged;
        }

        private void Awake() =>
            _text = GetComponent<TMP_Text>();

        private void Start()
        {
            if (_localizator != null && _localizator.Ready)
                Translate();
        }

        private void OnDestroy() =>
            _localizator.LanguageChanged -= OnLanguageChanged;

        private void OnLanguageChanged() =>
            Translate();

        private void Translate()
        {
            if (_localizator == null || _text == null)
                return;

            _text.text = _localizator.GetWord(_key);
        }
    }
}