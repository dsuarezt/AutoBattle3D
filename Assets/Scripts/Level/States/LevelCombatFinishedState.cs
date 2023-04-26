//-----------------------------------------------------------------------
// File name: LevelCombatFinishedState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 26, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Rounds;

namespace LitLab.CyberTitans.Level
{
    public class LevelCombatFinishedState : LevelStateBase
    {
        #region Constructors

        public LevelCombatFinishedState(LevelController levelController) : base(levelController)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            _levelController.OnCombatPhaseFinishedChannel?.RaiseEvent();

            RoundManagerSO roundManager = _levelController.RoundManager;
            roundManager.Reward();

            if (roundManager.IsTheLastRound)
            {
                _levelController.ChangeState(nameof(LevelFinishedState));
            }
            else
            {
                _levelController.ChangeState(nameof(LevelPreparationStartedState));
            }
        }

        #endregion
    }
}
