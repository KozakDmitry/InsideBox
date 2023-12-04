
using System;
using Infostructure.Factory;
using Scripts.Data;
using Scripts.Services.Randomizer;
using UnityEngine;

namespace Scripts.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath enemyDeath;
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;
        private IRandomService _random;


        private void Start()
        {
            enemyDeath.Happened += SpawnLoot;
        }


        public void Construct(IGameFactory gameFactory, IRandomService random)
        {
            _factory = gameFactory;
            _random = random;
        }

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();
            loot.transform.position = transform.position;


            var lootItem = GenerateLoot();

            loot.Initialize(lootItem);
        }

        private Loot GenerateLoot()
        {
            return new Loot()
            {
                value = _random.Next(_lootMin,_lootMax)
            };
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}
