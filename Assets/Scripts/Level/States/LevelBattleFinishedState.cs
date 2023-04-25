//-----------------------------------------------------------------------
// File name: LevelBattleFinishedState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 26, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Rounds;

namespace LitLab.CyberTitans.Level
{
    public class LevelBattleFinishedState : LevelStateBase
    {
        #region Constructors

        public LevelBattleFinishedState(LevelController levelController) : base(levelController)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            _levelController.OnBattlePhaseFinishedChannel?.RaiseEvent();
            RoundManagerSO roundManager = _levelController.RoundManager;
            roundManager.Reward();

            if (roundManager.IsTheLastRound)
            {
                _levelController.ChangeState(nameof(LevelFinishedState));
            }
            else
            {
                _levelController.EnemyBattlefieldController.DestroyEnemies();
                _levelController.ChangeState(nameof(LevelPreparationStartedState));
            }
        }

        #endregion
    }
}
