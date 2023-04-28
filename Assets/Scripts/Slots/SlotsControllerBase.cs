//-----------------------------------------------------------------------
// File name: SlotsControllerBase.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Shared;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Slots
{
    public abstract class SlotsControllerBase : MonoBehaviour, ISlotsController
    {
        #region Fields

        [SerializeField] protected Slot[] _slots = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] protected SlotEventChannelSO _onSelectCharacterChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] protected SlotEventChannelSO _onDeselectCharacterChannel = default;

        protected IList<Character> _characters = new List<Character>();
        protected IList<CharacterSelector> _characterSelectors = new List<CharacterSelector>();
        protected Character _characterSelected;

        #endregion

        #region Properties

        public IList<Character> Characters => _characters;

        #endregion

        #region Engine Methods

        protected virtual void Awake()
        {
            foreach (var slot in _slots)
            {
                slot.SlotController = this;
            }

            RegisterListeners();
        }

        protected virtual void OnDestroy()
        {
            UnregisterListeners();
        }

        #endregion

        #region Methods

        public abstract bool CanReceiveACharacter(Character character);

        public virtual void OnCharacterAddedToSlot(Character character)
        {
            if (!_characters.Contains(character))
            {
                _characters.Add(character);
                _characterSelectors.Add(character.GetComponent<CharacterSelector>());
            }
        }

        public virtual void OnCharacterRemovedFromSlot(Character character)
        {
            int index = _characters.IndexOf(character);
            _characters?.RemoveAt(index);
            _characterSelectors?.RemoveAt(index);
        }

        protected virtual void RegisterListeners()
        {
            _onSelectCharacterChannel.OnEventRaised += OnSelectCharacter;
            _onDeselectCharacterChannel.OnEventRaised += OnDeselectCharacter;
        }

        protected virtual void UnregisterListeners()
        {
            _onSelectCharacterChannel.OnEventRaised -= OnSelectCharacter;
            _onDeselectCharacterChannel.OnEventRaised -= OnDeselectCharacter;
        }

        protected virtual void OnSelectCharacter(object sender, Slot slot)
        {
            _characterSelected = slot.Character;
            ActivateSlots(true);
            AllowCharacterSelection(false);
        }

        protected virtual void OnDeselectCharacter(object sender, Slot slot)
        {
            _characterSelected = null;
            ActivateSlots(false);
            AllowCharacterSelection(true);
        }

        protected void ActivateSlots(bool value)
        {
            foreach (var slot in _slots)
            {
                slot.ActivateBase(value);
            }
        }

        protected void AllowCharacterSelection(bool value)
        {
            foreach (var characterSelector in _characterSelectors)
            {
                characterSelector.SetActive(value);
            }
        }

        #endregion
    }
}
