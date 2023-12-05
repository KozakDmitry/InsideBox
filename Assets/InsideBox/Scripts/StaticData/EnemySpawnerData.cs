using System;
using UnityEngine;

namespace Scripts.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string id;
        public MonsterTypeId monsterTypeId;
        public Vector3 position;

        public EnemySpawnerData(string id, MonsterTypeId argMonsterTypeId, Vector3 transformPosition)
        {
            this.id = id;
            this.monsterTypeId = argMonsterTypeId;
            this.position = transformPosition;
        }
    }
}