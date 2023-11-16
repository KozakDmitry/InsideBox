using Scripts.Data;

namespace Infostructure.Services.SaveLoad
{
    public interface ISaveLoadService :IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}