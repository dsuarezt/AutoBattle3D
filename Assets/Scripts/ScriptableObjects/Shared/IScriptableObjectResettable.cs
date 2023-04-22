//-----------------------------------------------------------------------
// File name: IScriptableObjectResettable.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

namespace LitLab.CyberTitans.Shared
{
    public interface IScriptableObjectResettable
	{
        #region Methods

        void ResetOnExitPlayMode();

        #endregion
    }
}
