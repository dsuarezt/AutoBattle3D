//-----------------------------------------------------------------------
// File name: LevelBattleState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using Cysharp.Threading.Tasks;
using System.Threading;

namespace LitLab.CyberTitans.Level
{
    public class LevelBattleState : LevelStateBase
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Constructors

        public LevelBattleState(LevelController levelController) : base(levelController)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            StartBattlePhaseAsync().Forget();
        }

        private async UniTask StartBattlePhaseAsync()
        {
            _levelController.OnBattlePhaseStartedChannel?.RaiseEvent();

            CancellationToken cancellationToken = _levelController.GetCancellationTokenOnDestroy();
            await _levelController.RoundManager.StartPreparationPhaseAsync(cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
            {
                if (_levelController.RoundManager.IsTheLastRound)
                {
                    // TODO:
                    // _levelController.ChangeState(nameof(LevelFinishedState));
                }
                else
                {
                    _levelController.ChangeState(nameof(LevelPreparationState));
                }
            }
        }

        #endregion
    }
}
