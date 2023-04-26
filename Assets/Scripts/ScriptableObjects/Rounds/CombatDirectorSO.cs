//-----------------------------------------------------------------------
// File name: CombatDirectorSO.cs
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
    [CreateAssetMenu(fileName = "CombatDirector", menuName = "CyberTitans/Rounds/Combat Director")]
    public class CombatDirectorSO : DescriptionBaseSO
    {
        #region Fields

        [SerializeField] private RoundSettingsSO _roundSettings = default;

        #endregion

        #region Methods

        public async UniTask<CombatResult> StartNewCombat(CancellationToken cancellationToken)
        {
            await UniTask.Delay
            (
                delayTimeSpan: TimeSpan.FromSeconds(_roundSettings.CombatTime),
                cancellationToken: cancellationToken
            );

            int num = UnityEngine.Random.Range(1,11);

            return num <= 5 ? CombatResult.Won : CombatResult.Lost;
        }

        #endregion
    }
}
