﻿using System;

namespace Scripts.Data
{
    [Serializable]
    public class LootData
    {
        public int collected;
        public Action ChangedLoot;

        public void Collect(Loot loot)
        {
            collected += loot.value;
            ChangedLoot?.Invoke();
        }
    }
}