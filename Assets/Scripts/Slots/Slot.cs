//-----------------------------------------------------------------------
// File name: Slot.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using System;
using GivingLife.Debugging;
using LitLab.CyberTitans.Characters;
using UnityEngine;

namespace LitLab.CyberTitans.Slots
{
    public class Slot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _base = default;
        [SerializeField] private Transform _spawnPoint = default;

        private Character _character;
        private CharacterSelector _characterSelector;

        #endregion

        #region Properties

        public bool IsEmpty => _character == null;
        public ISlotController SlotController { get; set; }
        public bool CanReceiveACharacter => SlotController.CanReceiveACharacter;

        #endregion

        #region Methods

        public void AddCharacter(Character character)
        {
            if (character)
            {
                // It is necessary to unsubscribe from the current character selection events.
                UnsubscribeToSelectionEvents();

                _character = character;
                _character?.transform.SetParent(_spawnPoint);
                _character?.transform.SetPositionAndRotation(_spawnPoint.position, _spawnPoint.rotation);

                if (_character && _character.TryGetComponent(out _characterSelector))
                {
                    _characterSelector.Slot = this;
                    SubscribeToSelectionEvents();
                }

                SlotController.OnCharacterAddedToSlot(_character);
            }
        }

        public void ActivateBase(bool value)
        {
            _base.SetActive(value);
        }

        private void SubscribeToSelectionEvents()
        {
            if (_character && _characterSelector)
            {
                _characterSelector.OnSelectEvent += OnCharacterSelect;
                _characterSelector.OnDeselectEvent += OnCharacterDeselect;
            }
        }

        private void UnsubscribeToSelectionEvents()
        {
            if (_character && _characterSelector)
            {
                _characterSelector.OnSelectEvent -= OnCharacterSelect;
                _characterSelector.OnDeselectEvent -= OnCharacterDeselect;
            }
        }

        private void OnCharacterSelect()
        {
            // TODO:
            GLDebug.Log($"Selecting the character {_character.name} from its Slot.", Color.green);
        }

        private void OnCharacterDeselect()
        {
            // TODO:
            GLDebug.Log($"Deselecting the character {_character.name} from its Slot.", Color.magenta);
        }

        #endregion
    }
}
