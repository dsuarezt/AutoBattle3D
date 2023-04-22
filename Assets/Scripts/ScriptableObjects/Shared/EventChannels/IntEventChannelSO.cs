//-----------------------------------------------------------------------
// File name: IntEventChannelSO.cs
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
    [CreateAssetMenu(fileName = "IntEventChannel", menuName = "CyberTitans/Events/Int Event Channel")]
    public class IntEventChannelSO : DescriptionBaseSO
	{
        #region Fields

#if UNITY_EDITOR
        [BoxGroup(AttributeConstants.EVENT_PARAMETERS)]
        [SerializeField] private int _value = default;
#endif

        #endregion

        #region Events

        public event Action<object, int> OnEventRaised;

        #endregion

        #region Methods

        public void RaiseEvent(object sender, int value)
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
