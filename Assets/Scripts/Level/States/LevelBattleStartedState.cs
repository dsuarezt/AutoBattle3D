//-----------------------------------------------------------------------
// File name: LevelBattleStartedState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 26, 2023
//-----------------------------------------------------------------------

using Cysharp.Threading.Tasks;
using System.Threading;

namespace LitLab.CyberTitans.Level
{
    public class LevelBattleStartedState : LevelStateBase 
	{
        #region Constructors

        public LevelBattleStartedState(LevelController levelController) : base(levelController)
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

            CancellationToken cancellationToken = _levelController.GetCancellationToken();
            await _levelController.RoundManager.StartBattlePhaseAsync(cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
            {
                _levelController.ChangeState(nameof(LevelBattleFinishedState));
            }
        }

        #endregion
    }
}
