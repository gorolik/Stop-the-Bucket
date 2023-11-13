using UnityEngine;
using YG;

namespace Sources.Services.Ads
{
    public class YandexAdsService : IAdsService
    {
        public void Init()
        {
            if (YandexGame.SDKEnabled)
                YandexGame.ErrorFullAdEvent += OnFullscreenAdError;
            else
                Debug.LogError("Yandex ads service is unaviable");
        }
        
        public void ShowFullscreenAd() => 
            YandexGame.FullscreenShow();

        public void OnFullscreenAdError() => 
            Debug.Log("Yandex fullscreen ad error");
    }
}