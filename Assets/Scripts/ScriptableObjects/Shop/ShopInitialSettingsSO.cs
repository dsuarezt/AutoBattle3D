//-----------------------------------------------------------------------
// File name: ShopInitialSettingsSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Shop
{
	[CreateAssetMenu(fileName = "ShopInitialSettings", menuName = "CyberTitans/Shop/Shop Initial Settings")]
    public class ShopInitialSettingsSO : DescriptionBaseSO
	{
        #region Constants



        #endregion

        #region Fields

        [SerializeField] private int _shopRefreshCost = default;

        #endregion

        #region Properties

        public int ShopRefreshCost => _shopRefreshCost;

        #endregion
    }
}
