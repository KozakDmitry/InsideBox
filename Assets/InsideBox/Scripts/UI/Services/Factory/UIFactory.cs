using Infostructure.AssetManagеment;
using Infostructure.Services.PersistentProgress;
using Scripts.Infostructure.Services.Ads;
using Scripts.StaticData;
using Scripts.StaticData.Windows;
using Scripts.UI.Services.Windows;
using Scripts.UI.Windows;
using Scripts.UI.Windows.Shop;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UIRoot";

        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly IAssets _assets;
        private readonly IAdsService _adsService;
        

        private Transform _UIRoot;

        public UIFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService progressService, IAdsService adsService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _adsService = adsService;
        }

        public void CreateShop() 
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            ShopWindow window = Object.Instantiate(config.prefab,_UIRoot) as ShopWindow;
            window.Construct(_adsService,_progressService);
        }

        public async Task CreateUIRoot()
        {
            GameObject root = await _assets.InstantiatePrefab(UIRootPath);
            _UIRoot = root.transform;
        }
    }
}
