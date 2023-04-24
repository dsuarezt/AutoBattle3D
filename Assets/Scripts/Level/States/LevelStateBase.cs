//-----------------------------------------------------------------------
// File name: LevelStateBase.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 24, 2023
//-----------------------------------------------------------------------

namespace LitLab.CyberTitans.Level
{
    public abstract class LevelStateBase: ILevelState
	{
        #region Fields

        protected LevelController _levelController;

        #endregion

        #region Constructors

        public LevelStateBase(LevelController levelController)
        {
            _levelController = levelController;
        }

        #endregion

        #region Methods

        public abstract void Enter();

        #endregion
    }
}
