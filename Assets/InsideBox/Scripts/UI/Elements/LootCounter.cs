using Scripts.Data;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Elements
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI Counter;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.ChangedLoot += UpdateCounter;
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            Counter.text = $"{_worldData.LootData.collected}";
        }
    }
}