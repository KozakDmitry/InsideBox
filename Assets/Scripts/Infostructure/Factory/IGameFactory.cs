using System;
using System.Collections.Generic;
using Infostructure.Services.PersistentProgress;
using UnityEngine;

namespace Infostructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        void CreateHUD();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }


        GameObject HeroGameObject { get; }
        event Action HeroCreated;
        
        void CleanUp();
    }
}