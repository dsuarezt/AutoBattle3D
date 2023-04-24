//-----------------------------------------------------------------------
// File name: ILevelStateConfiguration.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 24, 2023
//-----------------------------------------------------------------------

namespace LitLab.CyberTitans.Level
{
    public interface ILevelStateConfiguration
    {
        #region Methods

        void AddState(string stateName, ILevelState state);
        ILevelState GetState(string stateName);

        #endregion
    }
}
