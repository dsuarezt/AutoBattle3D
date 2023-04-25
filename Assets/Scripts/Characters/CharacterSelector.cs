//-----------------------------------------------------------------------
// File name: CharacterSelector.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

using System;
using GivingLife.Debugging;
using LitLab.CyberTitans.Slots;
using UnityEngine;

namespace LitLab.CyberTitans.Characters
{
    public class CharacterSelector : MonoBehaviour
    {
        #region Fields

        private Collider _collider;

        #endregion

        #region Properties

        public Slot Slot { get; set; }

        #endregion

        #region Events

        public event Action OnSelectEvent;
        public event Action OnDeselectEvent;

        #endregion

        #region Engine Methods

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnMouseDown()
        {
            GLDebug.Log($"Selecting the character {name}.", Color.green);
            OnSelectEvent?.Invoke();
        }

        private void OnMouseUp()
        {
            GLDebug.Log($"Deselecting the character {name}.", Color.magenta);
            OnDeselectEvent?.Invoke();
        }

        #endregion

        #region Methods

        public void SetActive(bool value)
        {
            _collider.enabled = value;
        }

        #endregion
    }
}
