//-----------------------------------------------------------------------
// File name: InventoryController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using System.Linq;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Shared;
using LitLab.CyberTitans.Slots;
using UnityEngine;

namespace LitLab.CyberTitans.Inventory
{
    public class InventoryController : MonoBehaviour, ISlotAuthorizer
    {
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private CharacterSpawnerSO _characterSpawner = default;

        [Space(5)]
        [SerializeField] private Slot[] _slots = default;

        #endregion

        #region Properties

        public bool AnyEmptySlot => GetFirstEmptySlot();
        public bool CanReceiveACharacter => true;

        #endregion

        #region Engine Methods

        private void Awake()
        {
            foreach (var slot in _slots)
            {
                slot.SlotAuthorizer = this;
            }
        }

        #endregion

        #region Methods

        public void AddCharacter(CharacterDataSO characterData)
        {
            Slot slot = GetFirstEmptySlot();

            if (slot)
            {
                Character character = _characterSpawner.SpawnCharacter(characterData);
                character?.gameObject.AddComponent<CharacterSelector>();
                slot.AddCharacter(character);
            }
        }

        private Slot GetFirstEmptySlot()
        {
            return _slots.FirstOrDefault(s => s.IsEmpty);
        }

        #endregion
    }
}
