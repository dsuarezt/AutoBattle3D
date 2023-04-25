//-----------------------------------------------------------------------
// File name: RoundSettingsSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Rounds
{
    [CreateAssetMenu(fileName = "RoundSettings", menuName = "CyberTitans/Rounds/Round Settings")]
    public class RoundSettingsSO : DescriptionBaseSO
    {
        #region Fields

        [SerializeField] private int _roundAmount = default;
        [SerializeField] private int _preparationTime = default;
        [SerializeField] private int _goldAmountPerWinStreak = default;
        [SerializeField] private int _goldAmountPerBattleWon = default;
        [SerializeField] private int _battleDuration = default;

        #endregion

        #region Properties

        public int RoundAmount => _roundAmount;
        public int PreparationTime => _preparationTime;
        public int GoldAmountPerWinStreak => _goldAmountPerWinStreak;
        public int GoldAmountPerBattleWon => _goldAmountPerBattleWon;
        public int BattleDuration => _battleDuration;

        #endregion
    }
}
