using Infostructure.AssetManagеment;
using Scripts.StaticData;
using Scripts.UI.Services.Windows;
using UnityEngine;

namespace Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private readonly IStaticDataService _staticData;
        private readonly IAssets _assets;
        private Transform _UIRoot;

        public UIFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public void CreateShop() 
        {
            var config = _staticData.ForWindow(WindowId.Shop);
        }

        public void CreateUIRoot() =>
            _UIRoot = _assets.InstantiatePrefab(UIRootPath).transform;
    }
}
