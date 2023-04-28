//-----------------------------------------------------------------------
// File name: LevelPreparationStartedState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 24, 2023
//-----------------------------------------------------------------------

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace LitLab.CyberTitans.Level
{
    public class LevelPreparationStartedState : LevelStateBase
    {
        #region Constructors

        public LevelPreparationStartedState(LevelController levelController) : base(levelController)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            _levelController.PreparationPhaseMessage.SetActive(true);
            StartPreparationPhaseAsync().Forget();
        }

        private async UniTask StartPreparationPhaseAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));

            _levelController.PreparationPhaseMessage.SetActive(false);
            _levelController.ActiveBattlefieldInputBlocker(false);

            _levelController.OnPreparationPhaseStartedChannel?.RaiseEvent(this);

            CancellationToken cancellationToken = _levelController.GetCancellationToken();
            await _levelController.RoundManager.StartPreparationPhaseAsync(cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
            {
                _levelController.ChangeState(nameof(LevelPreparationFinishedState));
            }
        }

        #endregion
    }
}
