//-----------------------------------------------------------------------
// File name: InventoryController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Shared;
using LitLab.CyberTitans.Slots;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Inventory
{
    public class InventoryController : MonoBehaviour, ISlotController
    {
        #region Fields

        [SerializeField] private Slot[] _slots = default;

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private CharacterSpawnerSO _characterSpawner = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private SlotEventChannelSO _onSelectCharacterChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private SlotEventChannelSO _onDeselectCharacterChannel = default;


        private IList<Character> _characters = new List<Character>();

        #endregion

        #region Properties

        public bool AnyEmptySlot => _slots.Length > _characters.Count;

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

        public void AddCharacter(CharacterDataSO characterData)
        {
            if (AnyEmptySlot)
            {
                Slot slot = GetFirstEmptySlot();

                if (slot)
                {
                    Character character = _characterSpawner.SpawnCharacter(characterData);
                    character?.gameObject.AddComponent<CharacterSelector>();
                    slot.AddNewCharacter(character);
                }
            }
        }

        public bool CanReceiveACharacter(Character character) => true;

        public void OnCharacterAddedToSlot(Character character)
        {
            if (!_characters.Contains(character))
            {
                _characters.Add(character);
            }
        }

        public void OnCharacterRemovedFromSlot(Character character)
        {
            _characters.Remove(character);
        }

        private Slot GetFirstEmptySlot()
        {
            return _slots.FirstOrDefault(s => s.IsEmpty);
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

        private void ActivateSlots(bool value)
        {
            foreach (var slot in _slots)
            {
                slot.ActivateBase(value);
            }
        }

        private void OnSelectCharacter(object sender, Slot slot)
        {
            ActivateSlots(true);
        }

        private void OnDeselectCharacter(object sender, Slot slot)
        {
            ActivateSlots(false);
        }

        #endregion
    }
}
