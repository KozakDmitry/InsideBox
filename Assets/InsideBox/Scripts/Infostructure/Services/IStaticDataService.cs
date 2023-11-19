using Scripts.StaticData;

namespace Infostructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
    }
}