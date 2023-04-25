//-----------------------------------------------------------------------
// File name: Slot.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using GivingLife.Debugging;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Shared;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Slots
{
    public class Slot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _base = default;
        [SerializeField] private Collider _baseCollider = default;
        [SerializeField] private Transform _spawnPoint = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private SlotEventChannelSO _onSelectCharacterChannel = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private SlotEventChannelSO _onDeselectCharacterChannel = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private SlotEventChannelSO _onMouseEnterChannel = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private SlotEventChannelSO _onMouseExitChannel = default;

        private Character _character;
        private CharacterSelector _characterSelector;

        #endregion

        #region Properties

        public Character Character => _character;
        public bool IsEmpty => _character == null;
        public ISlotsController SlotController { get; set; }

        #endregion

        #region Engine Methods

        private void Awake()
        {
            ActivateBase(false);
        }

        private void OnMouseEnter()
        {
            _onMouseEnterChannel?.RaiseEvent(this, this);
        }

        private void OnMouseExit()
        {
            _onMouseExitChannel?.RaiseEvent(this, this);
        }

        #endregion

        #region Methods

        public bool CanReceiveACharacter(Character character)
        {
            return SlotController.CanReceiveACharacter(character);
        }

        public void AddNewCharacter(Character character)
        {
            if (character)
            {
                // Before adding a new character it is necessary to remove the previous one in case it existed.
                RemoveCharacter();

                _character = character;
                _character?.transform.SetParent(_spawnPoint);
                ResetCharacter();

                if (_character && _character.TryGetComponent(out _characterSelector))
                {
                    _characterSelector.Slot = this;
                    SubscribeToSelectionEvents();
                }

                SlotController.OnCharacterAddedToSlot(_character);
            }
        }

        public void ResetCharacter()
        {
            _character?.transform.SetPositionAndRotation(_spawnPoint.position, _spawnPoint.rotation);
            _character.Reset();
        }

        public Character RemoveCharacter()
        {
            var character = _character;

            if (character)
            {
                UnsubscribeToSelectionEvents();
                _character = null;
                _characterSelector = null;
                SlotController.OnCharacterRemovedFromSlot(character);
            }

            return character;
        }

        public void ActivateBase(bool value)
        {
            _base.SetActive(value);
            _baseCollider.enabled = value;
        }

        private void SubscribeToSelectionEvents()
        {
            if (_character && _characterSelector)
            {
                _characterSelector.OnSelectEvent += OnSelectCharacter;
                _characterSelector.OnDeselectEvent += OnDeselectCharacter;
            }
        }

        private void UnsubscribeToSelectionEvents()
        {
            if (_character && _characterSelector)
            {
                _characterSelector.OnSelectEvent -= OnSelectCharacter;
                _characterSelector.OnDeselectEvent -= OnDeselectCharacter;
            }
        }

        private void OnSelectCharacter()
        {
            GLDebug.Log($"Selecting the character {_character.name} from its Slot.", Color.green);
            _onSelectCharacterChannel?.RaiseEvent(this, this);
        }

        private void OnDeselectCharacter()
        {
            GLDebug.Log($"Deselecting the character {_character.name} from its Slot.", Color.magenta);
            _onDeselectCharacterChannel?.RaiseEvent(this, this);
        }

        #endregion
    }
}
