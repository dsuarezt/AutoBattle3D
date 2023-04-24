//-----------------------------------------------------------------------
// File name: LevelPreparationState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 24, 2023
//-----------------------------------------------------------------------

using System.Threading;
using Cysharp.Threading.Tasks;
using LitLab.CyberTitans.Rounds;

namespace LitLab.CyberTitans.Level
{
    public class LevelPreparationState : LevelStateBase
    {
        #region Constructors

        public LevelPreparationState(LevelController levelController) : base(levelController)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            StartPreparationPhaseAsync().Forget();
        }

        private async UniTask StartPreparationPhaseAsync()
        {
            _levelController.OnPreparationPhaseStartedChannel?.RaiseEvent();

            CancellationToken cancellationToken = _levelController.GetCancellationTokenOnDestroy();
            await _levelController.RoundManager.StartPreparationPhaseAsync(cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
            {
                _levelController.ChangeState(nameof(LevelBattleState));
            }
        }

        #endregion
    }
}
