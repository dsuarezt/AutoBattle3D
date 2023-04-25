//-----------------------------------------------------------------------
// File name: LevelPreparationFinishedState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using Cysharp.Threading.Tasks;
using System.Threading;

namespace LitLab.CyberTitans.Level
{
    public class LevelPreparationFinishedState : LevelStateBase
    {
        #region Constructors

        public LevelPreparationFinishedState(LevelController levelController) : base(levelController)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            FinishPreparationPhaseAsync().Forget();
        }

        private async UniTask FinishPreparationPhaseAsync()
        {
            _levelController.OnPreparationPhaseFinishedChannel?.RaiseEvent();

            CancellationToken cancellationToken = _levelController.GetCancellationToken();
            await _levelController.RoundManager.FinishPreparationPhaseAsync(cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
            {
                if (_levelController.RoundManager.IsTheLastRound)
                {
                    // TODO:
                    // _levelController.ChangeState(nameof(LevelFinishedState));
                }
                else
                {
                    _levelController.ChangeState(nameof(LevelPreparationStartedState));
                }
            }
        }

        #endregion
    }
}
