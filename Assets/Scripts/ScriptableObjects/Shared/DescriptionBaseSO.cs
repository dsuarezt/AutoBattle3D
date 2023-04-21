//-----------------------------------------------------------------------
// File name: ScriptPostProcessor.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 21, 2023
//-----------------------------------------------------------------------

using UnityEngine;

namespace LitLab.CyberTitans.Shared
{
    public abstract class DescriptionBaseSO : ScriptableObject
    {
        #region Fields

#if UNITY_EDITOR
        [TextArea(1, 20)]
        [SerializeField] private string _description = default;
#endif
        #endregion
    }
}
