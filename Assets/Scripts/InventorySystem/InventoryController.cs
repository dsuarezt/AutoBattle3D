//-----------------------------------------------------------------------
// File name: InventoryController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using System;
using System.Linq;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Shared;
using LitLab.CyberTitans.Slots;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Inventory
{
    public class InventoryController : SlotsControllerBase
    {
        #region Fields

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private VoidEventChannelSO _onCancelSelectionChannel = default;

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private CharacterSpawnerSO _characterSpawner = default;

        #endregion

        #region Properties

        public bool AnyEmptySlot => _slots.Length > _characters.Count;

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

        public override bool CanReceiveACharacter(Character character) => true;

        protected override void RegisterListeners()
        {
            base.RegisterListeners();
            _onCancelSelectionChannel.OnEventRaised += OnCancelSelection;
        }

        protected override void UnregisterListeners()
        {
            base.UnregisterListeners();
            _onCancelSelectionChannel.OnEventRaised -= OnCancelSelection;
        }

        private void OnCancelSelection()
        {
            ActivateSlots(false);
        }

        private Slot GetFirstEmptySlot()
        {
            return _slots.FirstOrDefault(s => s.IsEmpty);
        }

        #endregion
    }
}
