using System;
using System.Linq;
using Scripts.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace Scripts.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueId)target;

            if (isPrefab(uniqueId))
            {
                return;
            }

            if (string.IsNullOrEmpty(uniqueId.Id))
            {
                Generate(uniqueId);
            }
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();
                if (uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                {
                    Generate(uniqueId);
                }
            }
        }

        private bool isPrefab(UniqueId uniqueId) => 
            uniqueId.gameObject.scene.rootCount == 0;

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid()}";

            if (Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);

            }

        }
    }
}