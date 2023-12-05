using System;
using System.Collections;
using Infostructure.Factory;
using Infostructure.Services.PersistentProgress;
using Scripts.Data;
using Scripts.Enemy;
using Scripts.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeID;
        public string id { get; set; }


        private bool _isSlain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public void Construct(IGameFactory factory) =>
            _factory = factory;

        private void OnDestroy()
        {

        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(id))
            {
                _isSlain = true;
            }
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(MonsterTypeID, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slain;

        }

        private void Slain()
        {
            if (_enemyDeath)
            {
                _enemyDeath.Happened -= Slain;
            }
          
            _isSlain = true;
        }


        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isSlain)
            {
                progress.KillData.ClearedSpawners.Add(id);
            }
        }

        
    }
}