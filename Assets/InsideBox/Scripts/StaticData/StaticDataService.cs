using System.Collections.Generic;
using System.Linq;
using Scripts.StaticData.Windows;
using Scripts.UI.Services.Windows;
using UnityEngine;


namespace Scripts.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.MonsterTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources
                 .Load<WindowStaticData>("StaticData/UI/WindowStaticData")
                 .Configs
                 .ToDictionary(x => x.windowId, x => x);

        }


        public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData)
                ? staticData
                : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;

        public WindowConfig ForWindow(WindowId windowId) => 
            _windowConfigs.TryGetValue(windowId, out WindowConfig staticData)
             ? staticData
             : null;
    }
}
