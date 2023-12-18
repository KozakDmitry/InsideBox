using TMPro;

namespace Scripts.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI SkullText;

        protected override void Initialize() =>
            RefreshSkullText();

        protected override void CleanUp()
        {
            base.CleanUp();
            Progress.worldData.LootData.ChangedLoot -= RefreshSkullText;
        }

        protected override void SubscribeUpdates() =>
            Progress.worldData.LootData.ChangedLoot += RefreshSkullText;

        private void RefreshSkullText() => 
            SkullText.text = Progress.worldData.LootData.collected.ToString();
    }
}