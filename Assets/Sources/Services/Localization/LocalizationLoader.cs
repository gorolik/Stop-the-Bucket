using System;
using System.Collections;
using System.IO;
using Sources.Infrastructure;
using UnityEngine;
using UnityEngine.Networking;

namespace Sources.Services.Localization
{
    public class LocalizationLoader
    {
        private const string _fileExtension = ".json";

        private readonly LocalizationStorage _localizationStorage;
        private readonly ICoroutineRunner _coroutineRunner;

        public LocalizationLoader(LocalizationStorage localizationStorage, ICoroutineRunner coroutineRunner)
        {
            _localizationStorage = localizationStorage;
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string langName, Action loaded) =>
            _coroutineRunner.StartCoroutine(LoadLocalizedText(langName, loaded));
        
        private IEnumerator LoadLocalizedText(string langName, Action loaded)
        {
            string path = Path.Combine(LocalizationPath.FolderPath, langName + _fileExtension);
            string dataAsJson;

            if (path.Contains("://") || path.Contains(":///"))
            {
                using (UnityWebRequest www = UnityWebRequest.Get(path))
                {
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                        dataAsJson = www.downloadHandler.text;
                    else
                    {
                        Debug.LogError("Language data load error " + www.error);
                        dataAsJson = www.error;
                    }
                }
            }
            else
            {
                dataAsJson = File.ReadAllText(path);
            }

            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            _localizationStorage.Load(loadedData);
            loaded?.Invoke();

            PlayerPrefs.SetString(LocalizationPath.DataKey, langName);
        }
    }
}