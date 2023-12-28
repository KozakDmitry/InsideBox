using System.Threading.Tasks;

namespace Scripts.UI.Services.Factory
{
    public interface IUIFactory :IService
    {
        Task CreateUIRoot();
        void CreateShop();
    }
}
