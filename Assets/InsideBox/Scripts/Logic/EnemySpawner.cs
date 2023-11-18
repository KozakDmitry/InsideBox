using System;
using System.Collections;
using Infostructure.Services.PersistentProgress;
using Scripts.Data;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeID;
        private string _id;


        private bool _isSlain;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
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