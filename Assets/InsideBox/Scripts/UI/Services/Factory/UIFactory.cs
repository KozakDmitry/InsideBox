using Infostructure.AssetManagеment;
using Infostructure.Services.PersistentProgress;
using Scripts.StaticData;
using Scripts.StaticData.Windows;
using Scripts.UI.Services.Windows;
using Scripts.UI.Windows;
using UnityEngine;

namespace Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";

        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly IAssets _assets;
        

        private Transform _UIRoot;

        public UIFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateShop() 
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.prefab,_UIRoot);
            window.Construct(_progressService);
        }

        public void CreateUIRoot() =>
            _UIRoot = _assets.InstantiatePrefab(UIRootPath).transform;
    }
}
