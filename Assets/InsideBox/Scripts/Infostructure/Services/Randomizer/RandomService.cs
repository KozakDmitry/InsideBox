﻿using Random = UnityEngine.Random;

namespace Scripts.Services.Randomizer
{
    public class RandomService : IRandomService
    {
        public int Next(int min, int max) =>
            Random.Range(min, max);
    }
}