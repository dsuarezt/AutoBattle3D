//-----------------------------------------------------------------------
// File name: LevelCombatStartedState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 26, 2023
//-----------------------------------------------------------------------

using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace LitLab.CyberTitans.Level
{
    public class LevelCombatStartedState : LevelStateBase 
	{
        #region Constructors

        public LevelCombatStartedState(LevelController levelController) : base(levelController)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            StartCombatPhaseAsync().Forget();
        }

        private async UniTask StartCombatPhaseAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));

            _levelController.CombatPhaseMessage.SetActive(false);
            _levelController.ActiveBattlefieldInputBlocker(false);

            _levelController.OnCombatPhaseStartedChannel?.RaiseEvent(this);

            CancellationToken cancellationToken = _levelController.GetCancellationToken();
            await _levelController.RoundManager.StartCombatPhaseAsync
            (
                _levelController.BattlefieldController.Characters,
                cancellationToken
            );

            if (!cancellationToken.IsCancellationRequested)
            {
                _levelController.ChangeState(nameof(LevelCombatFinishedState));
            }
        }

        #endregion
    }
}
