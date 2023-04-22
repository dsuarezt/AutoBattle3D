//-----------------------------------------------------------------------
// File name: ScriptableObjectsResetter.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEditor;
using UnityEngine;

namespace LitLab.CyberTitans.Editor
{
    public static class ScriptableObjectsResetter
	{
        #region Methods

        [InitializeOnLoadMethod]
        private static void RegisterResets()
        {
            EditorApplication.playModeStateChanged -= ResetScriptableObjectsOnExitPlayMode;
            EditorApplication.playModeStateChanged += ResetScriptableObjectsOnExitPlayMode;
        }

        private static void ResetScriptableObjectsOnExitPlayMode(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                var assets = FindAssets<ScriptableObject>();

                foreach (var a in assets)
                {
                    if (a is IScriptableObjectResettable resettable)
                    {
                       resettable.ResetOnExitPlayMode();
                    }
                }
            }
        }

        private static T[] FindAssets<T>() where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            var assets = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return assets;
        }

        #endregion
    }
}