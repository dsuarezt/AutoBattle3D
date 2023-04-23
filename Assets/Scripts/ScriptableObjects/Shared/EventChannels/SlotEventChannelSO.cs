//-----------------------------------------------------------------------
// File name: SlotEventChannelSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

using System;
using GivingLife.Debugging;
using LitLab.CyberTitans.Slots;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Shared
{
    [CreateAssetMenu(fileName = "SlotEventChannel", menuName = "CyberTitans/Events/Slot Event Channel")]
    public class SlotEventChannelSO : DescriptionBaseSO
	{
        #region Fields

#if UNITY_EDITOR
        [BoxGroup(AttributeConstants.EVENT_PARAMETERS)]
        [SerializeField] private Slot _value = default;
#endif

        #endregion

        #region Events

        public event Action<object, Slot> OnEventRaised;

        #endregion

        #region Methods

        public void RaiseEvent(object sender, Slot value)
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
