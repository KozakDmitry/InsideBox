using Infostructure.Services.PersistentProgress;
using Scripts.Infostructure.Services.Ads;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Shop
{
    public class RewarderAdItem : MonoBehaviour
    {
        public Button ShowAdVideoButton;
        public GameObject[] AdActiveObjects;
        public GameObject[] AdInActiveObjects;
        private IAdsService _adsService;
        private IPersistentProgressService _progressService;

        public void Initialize()
        {
            ShowAdVideoButton.onClick.AddListener(OnShowAdClicked);

            RefreshAvailableAd();
        }

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;
        }

        public void Subscribe() =>
            IronSourceEvents.onRewardedVideoAdReadyEvent += RefreshAvailableAd;
        public void CleanUp() =>
            IronSourceEvents.onRewardedVideoAdReadyEvent -= RefreshAvailableAd;
        
        private void RefreshAvailableAd()
        {
            bool videoReady = IronSource.Agent.isRewardedVideoAvailable();
            foreach(GameObject ad in AdActiveObjects)
            {
                ad.SetActive(videoReady);
            }
            foreach(GameObject ad in AdInActiveObjects)
            {
                ad.SetActive(!videoReady);
            }

        }
        private void OnVideoFinished()
        {
            _progressService.Progress.worldData.LootData.Add(_adsService.Reward);
        }
        private void OnShowAdClicked()
        {
            IronSource.Agent.showRewardedVideo();
            IronSourceEvents.onRewardedVideoAdEndedEvent += OnVideoFinished;
        }



    }
}