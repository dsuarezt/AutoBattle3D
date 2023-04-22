//-----------------------------------------------------------------------
// File name: BoolEventChannelSO.cs
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
    [CreateAssetMenu(fileName = "BoolEventChannel", menuName = "CyberTitans/Events/Bool Event Channel")]
    public class BoolEventChannelSO : DescriptionBaseSO
    {
        #region Fields

#if UNITY_EDITOR
        [BoxGroup(AttributeConstants.EVENT_PARAMETERS)]
        [SerializeField] private bool _value = default;
#endif

        #endregion

        #region Events

        public event Action<object, bool> OnEventRaised;

        #endregion

        #region Methods
        
        public void RaiseEvent(object sender, bool value)
        {
            GLDebug.Log($"Raising event: {name}. Args: {value}.");

            OnEventRaised?.Invoke(sender, value);
        }

#if UNITY_EDITOR
        [Button]
        private void Raise()
        {
            RaiseEvent(null, _value);
        }
#endif

        #endregion
    }
}
