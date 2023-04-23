//-----------------------------------------------------------------------
// File name: InventoryController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Shared;
using LitLab.CyberTitans.Slots;
using UnityEngine;

namespace LitLab.CyberTitans.Inventory
{
    public class InventoryController : MonoBehaviour, ISlotController
    {
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private CharacterSpawnerSO _characterSpawner = default;

        [Space(5)]
        [SerializeField] private Slot[] _slots = default;

        private IList<Character> _characters = new List<Character>();

        #endregion

        #region Properties

        public bool AnyEmptySlot => _slots.Length > _characters.Count;
        public bool CanReceiveACharacter => true;

        #endregion

        #region Engine Methods

        private void Awake()
        {
            foreach (var slot in _slots)
            {
                slot.SlotController = this;
            }
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
                    slot.AddCharacter(character);
                }
            }
        }

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

        #endregion
    }
}
