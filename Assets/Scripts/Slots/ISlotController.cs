//-----------------------------------------------------------------------
// File name: ISlotController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Characters;

namespace LitLab.CyberTitans.Slots
{
    public interface ISlotController
    {
        #region Properties

        bool CanReceiveACharacter { get; }

        #endregion

        #region Methods

        void OnCharacterAddedToSlot(Character character);
        void OnCharacterRemovedFromSlot(Character character);

        #endregion
    }
}
