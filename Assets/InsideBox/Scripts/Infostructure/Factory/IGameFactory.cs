﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infostructure.Services.PersistentProgress;
using Scripts.Enemy;
using Scripts.StaticData;
using UnityEngine;

namespace Infostructure.Factory
{
    public interface IGameFactory : IService
    {
        Task<GameObject> CreateHeroAsync(Vector3 at);
        Task<GameObject> CreateHUD();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        Task CreateSpawner(Vector3 at, string spawnerID, MonsterTypeId spawnerMonsterTypeId);

        void CleanUp();
        Task<GameObject> CreateMonster(MonsterTypeId monsterTypeID, Transform parent);
        Task WarmUp();
        Task<LootPiece> CreateLoot();
    }
}