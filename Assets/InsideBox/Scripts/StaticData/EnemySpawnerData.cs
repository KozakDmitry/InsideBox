using System;
using UnityEngine;

namespace Scripts.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public int id;
        public MonsterTypeId monsterTypeId;
        public Vector3 position;
    }
}