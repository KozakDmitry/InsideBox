using Infostructure.Services.PersistentProgress;
using Scripts.Infostructure.Services.Ads;
using TMPro;

namespace Scripts.UI.Windows.Shop
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI SkullText;
        public RewarderAdItem AdItem;
        protected override void Initialize()
        {
            AdItem.Initialize();
            RefreshSkullText();
        }

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            base.Construct(progressService);
            AdItem.Construct(adsService, progressService);
        }
        protected override void SubscribeUpdates()
        {
            AdItem.Subscribe();
            Progress.worldData.LootData.ChangedLoot += RefreshSkullText;
        }


        protected override void CleanUp()
        {
            base.CleanUp();
            AdItem.CleanUp();
            Progress.worldData.LootData.ChangedLoot -= RefreshSkullText;
        }

        private void RefreshSkullText() => 
            SkullText.text = Progress.worldData.LootData.collected.ToString();
    }
}