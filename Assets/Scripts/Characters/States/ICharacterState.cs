//-----------------------------------------------------------------------
// File name: ICharacterState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 27, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;

namespace LitLab.CyberTitans.Characters
{
    public interface ICharacterState : IState
	{
        #region Methods

        void Reset();
        void Combat();
        void OnDestroy();

        #endregion
    }
}
