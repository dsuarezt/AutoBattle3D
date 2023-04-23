//-----------------------------------------------------------------------
// File name: BattlefieldController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Level;
using LitLab.CyberTitans.Shared;
using LitLab.CyberTitans.Slots;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Battlefield
{
    public class BattlefieldController : MonoBehaviour, ISlotController
    {
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private SlotEventChannelSO _onSelectCharacterChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private SlotEventChannelSO _onDeselectCharacterChannel = default;

        [Space(5)]
        [SerializeField] private Slot[] _slots = default;

        private IList<Character> _characters = new List<Character>();

        #endregion

        #region Engine Methods

        private void Awake()
        {
            foreach (var slot in _slots)
            {
                slot.SlotController = this;
            }

            RegisterListeners();
        }

        private void OnDestroy()
        {
            UnregisterListeners();
        }

        #endregion

        #region Methods

        public bool CanReceiveACharacter(Character character)
        {
           return Contains(character) || _characters.Count < _levelEconomyManager.PlayerLevel;
        }

        public void OnCharacterAddedToSlot(Character character)
        {
            if (!Contains(character))
            {
                _characters.Add(character);
            }
        }

        public void OnCharacterRemovedFromSlot(Character character)
        {
            _characters.Remove(character);
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
            ActivateSlots(true);
        }

        private void OnDeselectCharacter(object sender, Slot slot)
        {
            ActivateSlots(false);
        }

        private void ActivateSlots(bool value)
        {
            foreach (var slot in _slots)
            {
                slot.ActivateBase(value);
            }
        }

        private bool Contains(Character character)
        {
            return _characters.Contains(character);
        }

        #endregion
    }
}
