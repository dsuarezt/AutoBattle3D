//-----------------------------------------------------------------------
// File name: CharacterSelector.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

using System;
using GivingLife.Debugging;
using LitLab.CyberTitans.Shared;
using LitLab.CyberTitans.Slots;
using UnityEngine;

namespace LitLab.CyberTitans.Characters
{
    public class CharacterSelector : MonoBehaviour
    {
        #region Fields

        private Collider _collider;
        private SlotEventChannelSO _onSelectCharacterChannel; // Listening on
        private SlotEventChannelSO _onDeselectCharacterChannel; // Listening on

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

        private void OnDestroy()
        {
            UnregisterListeners();
        }

        #endregion

        #region Methods

        public void Initialize(SlotEventChannelSO onSelectCharacterChannel,
                               SlotEventChannelSO onDeselectCharacterChannel)
        {
            _onSelectCharacterChannel = onSelectCharacterChannel;
            _onDeselectCharacterChannel = onDeselectCharacterChannel;
            RegisterListeners();
        }

        private void RegisterListeners()
        {
            _onSelectCharacterChannel.OnEventRaised += OnSelectCharacter;
            _onDeselectCharacterChannel.OnEventRaised += OnDeselectCharacter;
        }

        private void UnregisterListeners()
        {
            _onSelectCharacterChannel.OnEventRaised -= OnSelectCharacter;
            _onDeselectCharacterChannel.OnEventRaised -= OnDeselectCharacter;
        }

        private void OnSelectCharacter(object sender, Slot slot)
        {
            _collider.enabled = false;
        }

        private void OnDeselectCharacter(object sender, Slot slot)
        {
            _collider.enabled = true;
        }

        #endregion
    }
}
