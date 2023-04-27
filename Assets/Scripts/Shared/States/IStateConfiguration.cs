//-----------------------------------------------------------------------
// File name: IStateConfiguration.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 27, 2023
//-----------------------------------------------------------------------

namespace LitLab.CyberTitans.Shared
{
    public interface IStateConfiguration
	{
        #region Methods

        void AddState(string stateName, IState state);
        IState GetState(string stateName);

        #endregion
    }
}
