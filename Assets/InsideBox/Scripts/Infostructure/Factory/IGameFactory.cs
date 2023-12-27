using System;
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
        GameObject CreateHero(Vector3 at);
        GameObject CreateHUD();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CreateSpawner(Vector3 at, string spawnerID, MonsterTypeId spawnerMonsterTypeId);

        void CleanUp();
        Task<GameObject> CreateMonster(MonsterTypeId monsterTypeID, Transform parent);
        LootPiece CreateLoot();
    }
}