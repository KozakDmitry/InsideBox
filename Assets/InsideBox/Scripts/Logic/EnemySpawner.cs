using System;
using System.Collections;
using Infostructure.Factory;
using Infostructure.Services.PersistentProgress;
using Scripts.Data;
using Scripts.Enemy;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeID;
        public string _id;


        private bool _isSlain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        private void OnDestroy()
        {

        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
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
                progress.KillData.ClearedSpawners.Add(_id);
            }
        }
    }
}