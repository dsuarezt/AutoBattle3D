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
using UnityEngine;

namespace LitLab.CyberTitans.Battlefield
{
    public class BattlefieldController : MonoBehaviour, ISlotController
    {
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;

        [Space(5)]
        [SerializeField] private Slot[] _slots = default;

        private IList<Character> _characters = new List<Character>();

        #endregion

        #region Properties

        public bool CanReceiveACharacter => _characters.Count < _levelEconomyManager.PlayerLevel;

        #endregion

        #region Engine Methods



        #endregion

        #region Methods

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

        #endregion
    }
}
