//-----------------------------------------------------------------------
// File name: ISlotsController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;
using LitLab.CyberTitans.Characters;

namespace LitLab.CyberTitans.Slots
{
    public interface ISlotsController
    {
        #region Properties

        public IList<Character> Characters { get; }

        #endregion

        #region Methods

        bool CanReceiveACharacter(Character character);
        void OnCharacterAddedToSlot(Character character);
        void OnCharacterRemovedFromSlot(Character character);

        #endregion
    }
}
