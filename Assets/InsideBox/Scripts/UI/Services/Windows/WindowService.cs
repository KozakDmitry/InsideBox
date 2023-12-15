using Scripts.UI.Services.Factory;

namespace Scripts.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private IUIFactory _UIFactory;

        public WindowService(IUIFactory UIFactory)
        {
            _UIFactory = UIFactory;
        }
        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Shop:
                    _UIFactory.CreateShop();
                    break;
            }
        }
    }
}
