//-----------------------------------------------------------------------
// File name: CombatDirectorSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitLab.CyberTitans.Characters;
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

        public async UniTask<CombatResult> StartNewCombatAsync(IList<Character> characters,
                                                               CancellationToken cancellationToken)
        {
            await UniTask.Delay
            (
                delayTimeSpan: TimeSpan.FromSeconds(_roundSettings.CombatTime),
                cancellationToken: cancellationToken
            );

            var result = CombatResult.None;

            if (!cancellationToken.IsCancellationRequested)
            {
                if (characters.Count == 0)
                {
                    result = CombatResult.Lost;
                }
                else
                {
                    int num = UnityEngine.Random.Range(1, 11);
                    result = num <= 5 ? CombatResult.Won : CombatResult.Lost;
                }
            }

            return result;
        }

        #endregion
    }
}
