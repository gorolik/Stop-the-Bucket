using UnityEngine;

namespace Sources.Services.Ads
{
    public class NoAdsService : IAdsService
    {
        public void Init() => 
            Debug.Log("Ads disabled");

        public void ShowFullscreenAd() { }
    }
}