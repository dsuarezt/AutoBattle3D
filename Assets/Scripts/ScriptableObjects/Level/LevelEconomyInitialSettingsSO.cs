//-----------------------------------------------------------------------
// File name: LevelEconomyInitialSettingsSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Level
{
    [CreateAssetMenu(fileName = "LevelEconomyInitialSettings",
        menuName = "CyberTitans/Level/Level Economy Initial Settings")]
    public class LevelEconomyInitialSettingsSO : DescriptionBaseSO
    {
        #region Fields

        [SerializeField] private int _goldAmount = default;
        [SerializeField] private int _livesAmount = default;
        [SerializeField] private int _playerLevel = default;
        [SerializeField] private int _playerLevelUpgradeCost = default;

        #endregion

        #region Properties

        public int GoldAmount => _goldAmount;
        public int LivesAmount => _livesAmount;
        public int PlayerLevel => _playerLevel;
        public int PlayerLevelUpgradeCost => _playerLevelUpgradeCost;

        #endregion
    }
}
