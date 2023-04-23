//-----------------------------------------------------------------------
// File name: Vector2EventChannelSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

using GivingLife.Debugging;
using NaughtyAttributes;
using System;
using UnityEngine;

namespace LitLab.CyberTitans.Shared
{
    [CreateAssetMenu(fileName = "Vector2EventChannel", menuName = "CyberTitans/Events/Vector2 Event Channel")]
    public class Vector2EventChannelSO : DescriptionBaseSO
	{
        #region Fields

#if UNITY_EDITOR
        [BoxGroup(AttributeConstants.EVENT_PARAMETERS)]
        [SerializeField] private Vector2 _value = default;
#endif

        #endregion

        #region Events

        public event Action<object, Vector2> OnEventRaised;

        #endregion

        #region Methods

        public void RaiseEvent(object sender, Vector2 value)
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
