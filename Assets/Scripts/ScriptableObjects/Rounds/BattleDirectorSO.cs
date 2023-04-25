//-----------------------------------------------------------------------
// File name: BattleDirectorSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Rounds
{
    [CreateAssetMenu(fileName = "BattleDirector", menuName = "CyberTitans/Rounds/Battle Director")]
    public class BattleDirectorSO : DescriptionBaseSO
    {
        #region Fields

        [SerializeField] private RoundSettingsSO _roundSettings = default;

        #endregion

        #region Methods

        public async UniTask<BattleResult> StartNewBattle(CancellationToken cancellationToken)
        {
            await UniTask.Delay
            (
                delayTimeSpan: TimeSpan.FromSeconds(_roundSettings.BattleDuration),
                cancellationToken: cancellationToken
            );

            int num = UnityEngine.Random.Range(0,21);

            return num <= 10 ? BattleResult.Won : BattleResult.Lost;
        }

        #endregion
    }
}
