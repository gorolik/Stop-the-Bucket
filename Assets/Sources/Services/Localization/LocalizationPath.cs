using System.IO;
using UnityEngine;

namespace Sources.Services.Localization
{
    public static class LocalizationPath
    {
        public static string FolderPath = Path.Combine(Application.streamingAssetsPath, LanguagesFolderName);
        
        public const string LanguagesFolderName = "Languages";
        public const string DataKey = "Language";
    }
}