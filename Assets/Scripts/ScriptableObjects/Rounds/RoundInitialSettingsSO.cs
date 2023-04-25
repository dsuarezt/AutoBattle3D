//-----------------------------------------------------------------------
// File name: RoundInitialSettingsSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Rounds
{
    [CreateAssetMenu(fileName = "RoundInitialSettings", menuName = "CyberTitans/Rounds/Round Initial Settings")]
    public class RoundInitialSettingsSO : DescriptionBaseSO
    {
        #region Fields

        [SerializeField] private int _roundAmount = default;
        [SerializeField] private int _preparationTime = default;

        #endregion

        #region Properties

        public int RoundAmount => _roundAmount;
        public int PreparationTime => _preparationTime;

        #endregion
    }
}
