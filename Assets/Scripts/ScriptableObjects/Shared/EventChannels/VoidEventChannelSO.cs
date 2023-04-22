//-----------------------------------------------------------------------
// File name: VoidEventChannelSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using System;
using GivingLife.Debugging;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Shared
{
    [CreateAssetMenu(fileName = "VoidEventChannel", menuName = "CyberTitans/Events/Void Event Channel")]
    public class VoidEventChannelSO : DescriptionBaseSO
    {
        #region Events

        public event Action OnEventRaised;

        #endregion

        #region Methods

        public void RaiseEvent()
        {
            GLDebug.Log($"Raising event: {name}.");

            OnEventRaised?.Invoke();
        }

#if UNITY_EDITOR
        [Button]
        private void Raise()
        {
            RaiseEvent();
        }
#endif

        #endregion
    }
}
