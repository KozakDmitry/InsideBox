﻿using Scripts.Logic.EnemySpawners;
using Scripts.StaticData;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Scripts.Logic;
using UnityEngine.SceneManagement;

namespace Scripts.Editor
{
    

    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialPointTag = "InitialPoint";
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners =
                    FindObjectsOfType<SpawnMarker>()
                        .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position))
                        .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
                levelData.InitialHeroPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
            }



            EditorUtility.SetDirty(target);
        }
    }
}