//-----------------------------------------------------------------------
// File name: Shop.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using GivingLife.Debugging;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Level;
using TMPro;
using UnityEngine;

namespace LitLab.CyberTitans.Shop
{
    public class Shop : MonoBehaviour
    {
        #region Constants



        #endregion

        #region Fields

        [SerializeField] private ShopInitialSettingsSO _shopInitialSettings = default;
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;
        [SerializeField] private CharacterDataProviderSO _characterDataProvider = default;
        [SerializeField] private TMP_Text _refreshCostText = default;
        [SerializeField] private CharacterCard[] _characterCards = default;

        private int _shopRefreshCost;

        #endregion

        #region Properties



        #endregion

        #region Engine Methods

        private void Start()
        {
            _shopRefreshCost = _shopInitialSettings.ShopRefreshCost;
            _refreshCostText.text = _shopRefreshCost.ToString();
            GenerateNewCards();
        }

        #endregion

        #region Methods

        public void Refresh()
        {
            bool success = _levelEconomyManager.TryMakePayment(_shopRefreshCost);

            if (success)
            {
                GenerateNewCards();
                GLDebug.Log("Generating new cards.", Color.green);
            }
            else
            {
                GLDebug.Log("There is not enough gold to refresh the store.");
            }
        }

        public bool TryBuyCharacter(CharacterDataSO characterData)
        {
            // TODO: Check if there is enough gold and if there are empty slots in the inventory.
            bool canBuyCharacter = _levelEconomyManager.TryMakePayment(characterData.Cost);

            if (canBuyCharacter)
            {
                // TODO: Put the character on the inventory.
                GLDebug.Log($"Putting the character {characterData.CharacterName} on the inventory.", Color.green);
            }
            else
            {
                GLDebug.Log("There is not enough gold or space in the inventory to buy a character.");
            }

            return canBuyCharacter;
        }

        private void GenerateNewCards()
        {
            CharacterDataSO characterData;

            foreach (var card in _characterCards)
            {
                characterData = _characterDataProvider.GetRandomCharacterData();

                if (characterData) card.Refresh(characterData);
            }
        }

        #endregion
    }
}
