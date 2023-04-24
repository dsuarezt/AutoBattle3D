//-----------------------------------------------------------------------
// File name: RaritySO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 21, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Characters
{
    [CreateAssetMenu(fileName = "Rarity", menuName = "CyberTitans/Characters/Rarity")]
    public class RaritySO : DescriptionBaseSO
    {
        #region Fields

        [SerializeField] private string _name = default;
        [SerializeField] private int _cost = default;

        #endregion

        #region Properties

        public string Name => _name;
        public int Cost => _cost;

        #endregion
    }
}
