//-----------------------------------------------------------------------
// File name: LevelPreparationFinishedState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System.Threading;
using Cysharp.Threading.Tasks;

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
            _levelController.CombatPhaseMessage.SetActive(true);
            _levelController.ActiveBattlefieldInputBlocker(true);
            FinishPreparationPhaseAsync().Forget();
        }

        private async UniTask FinishPreparationPhaseAsync()
        {
            _levelController.OnPreparationPhaseFinishedChannel?.RaiseEvent();
            
            _levelController.BattlefieldController.CheckCharacterAmountOnBattlefield();
            CancellationToken cancellationToken = _levelController.GetCancellationToken();
            await _levelController.EnemyBattlefieldController.GenerateEnemiesAsync(cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
            {
                _levelController.ChangeState(nameof(LevelCombatStartedState));
            }
        }

        #endregion
    }
}
