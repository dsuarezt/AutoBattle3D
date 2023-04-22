//-----------------------------------------------------------------------
// File name: Shop.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Characters;
using UnityEngine;

namespace LitLab.CyberTitans.Shop
{
    public class Shop : MonoBehaviour
    {
        #region Constants



        #endregion

        #region Fields

        [SerializeField] private CharacterDataProviderSO _characterDataProvider = default;

        // TODO:
        [SerializeField] private CharacterCard[] _characterCards = default;

        #endregion

        #region Properties



        #endregion

        #region Engine Methods



        #endregion

        #region Methods

        public void Refresh()
        {
            // TODO: Check if there is enough gold.
            if (true)
            {
                foreach (var card in _characterCards)
                {
                    card.Refresh(_characterDataProvider.GetRandomCharacterData());
                }
            }
        }

        #endregion
    }
}
