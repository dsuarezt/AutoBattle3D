//-----------------------------------------------------------------------
// File name: ISlotAuthorizer.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

namespace LitLab.CyberTitans.Slots
{
    public interface ISlotAuthorizer
    {
        #region Properties

        bool CanReceiveACharacter { get; }

        #endregion
    }
}
