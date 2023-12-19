using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Infostructure.Services.Ads
{
    public class AdsService : IAdsService
    {

#if UNITY_ANDROID
        string appKey = "1ceaceedd";
#elif UNITY_IPHONE
        string appKey = "8545d445";
#else
        string appKey = "unexpected_platform";
#endif


        private const string AndroidGameId = "5503674";
        private const string IOSGameId = "5503675";

        private const string RewardedVideoPlacementId = "Rewarded_Android";
        private const string RewardedInterstitialPlacementId = "Interstitial_Android";
        private const string RewardedBannerPlacementId = "Android_Banner_Ad";

        public int Reward => 13;

        public void Initialize()
        {
            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(appKey);
            IronSourceEvents.onSdkInitializationCompletedEvent += FinishedInitialize;
        }

        private void FinishedInitialize()
        {

        }
    }
}
