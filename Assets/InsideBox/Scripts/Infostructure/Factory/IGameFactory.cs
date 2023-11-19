using System;
using System.Collections.Generic;
using Infostructure.Services.PersistentProgress;
using Scripts.StaticData;
using UnityEngine;

namespace Infostructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHUD();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        public void Register(ISavedProgressReader progressReader);

        void CleanUp();
        GameObject CreateMonster(MonsterTypeId monsterTypeID, Transform parent);
    }
}