//-----------------------------------------------------------------------
// File name: Inventory.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using System.Linq;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Level;
using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private CharacterSpawnerSO _characterSpawner = default;

        [Space(5)]
        [SerializeField] private Slot[] _slots = default;

        #endregion

        #region Properties

        public bool AnyEmptySlot => GetFirstEmptySlot();

        #endregion

        #region Engine Methods



        #endregion

        #region Methods

        public void AddCharacter(CharacterDataSO characterData)
        {
            Slot slot = GetFirstEmptySlot();

            if (slot)
            {
                Character character = _characterSpawner.SpawnCharacter(characterData);
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
