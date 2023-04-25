//-----------------------------------------------------------------------
// File name: EnemyBattlefieldSettingsSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Battlefield
{
    [CreateAssetMenu(fileName = "EnemyBattlefieldSettings",
                     menuName = "CyberTitans/Battlefield/Enemy Battlefield Settings")]
    public class EnemyBattlefieldSettingsSO : DescriptionBaseSO
    {
        #region Fields

        [Header(AttributeConstants.ENEMY_GENERATION)]
        [SerializeField] private int _minEnemyAmount = default;
        [SerializeField] private int _maxEnemyAmount = default;
        [SerializeField] private int _generationDelay = default;

        #endregion

        #region Properties

        public int MinEnemyAmount => _minEnemyAmount;
        public int MaxEnemyAmount => _maxEnemyAmount;
        public int GenerationDelay => _generationDelay;

        #endregion
    }
}
