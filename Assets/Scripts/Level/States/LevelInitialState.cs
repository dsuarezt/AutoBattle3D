//-----------------------------------------------------------------------
// File name: LevelInitialState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 24, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Level;

namespace LitLab.CyberTitans
{
    public class LevelInitialState : LevelStateBase
    {
        #region Constructors

        public LevelInitialState(LevelController levelController) : base(levelController)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            _levelController.Initialize();
            _levelController.ActiveBattlefieldBlocker(false);
            _levelController.ChangeState(nameof(LevelPreparationStartedState));
        }

        #endregion
    }
}
