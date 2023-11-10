using UnityEngine;

namespace Scripts.Infostructure
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject initialPoint);
        void CreateHUD();
    }
}