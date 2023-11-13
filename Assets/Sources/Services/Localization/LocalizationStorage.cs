using System.Collections.Generic;

namespace Sources.Services.Localization
{
    public class LocalizationStorage
    {
        private Dictionary<string, string> _localizedText;

        public Dictionary<string, string> LocalizedText => _localizedText;

        public void Load(LocalizationData localizationData)
        {
            _localizedText = new Dictionary<string, string>();

            foreach (var word in localizationData.items)
                _localizedText.Add(word.key, word.value);
        }
    }
}