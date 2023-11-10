using UnityEngine;

namespace Infostructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        void CreateHUD();
    }
}