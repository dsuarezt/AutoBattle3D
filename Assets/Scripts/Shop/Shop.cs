//-----------------------------------------------------------------------
// File name: Shop.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using GivingLife.Debugging;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Inventory;
using LitLab.CyberTitans.Level;
using LitLab.CyberTitans.Shared;
using TMPro;
using UnityEngine;

namespace LitLab.CyberTitans.Shop
{
    public class Shop : MonoBehaviour
    {
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private ShopInitialSettingsSO _shopInitialSettings = default;
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;
        [SerializeField] private CharacterDataProviderSO _characterDataProvider = default;

        [Header(AttributeConstants.UI_ELEMENTS)]
        [SerializeField] private TMP_Text _refreshCostText = default;
        [SerializeField] private CharacterCard[] _characterCards = default;

        [Header(AttributeConstants.INVENTORY_SYSTEM)]
        [SerializeField] private InventoryController _inventory = default;

        private int _shopRefreshCost;

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
                GLDebug.Log("There is not enough gold to refresh the store.", Color.red);
            }
        }

        public bool TryBuyCharacter(CharacterDataSO characterData)
        {
            if (_inventory.AnyEmptySlot)
            {
                bool wasPaymentSuccessful = _levelEconomyManager.TryMakePayment(characterData.Cost);

                if (wasPaymentSuccessful)
                {
                    _inventory.AddCharacter(characterData);
                    GLDebug.Log($"Putting the character {characterData.CharacterName} on the inventory.", Color.green);

                    return true;
                }
            }

            return false;
        }

        private void GenerateNewCards()
        {
            CharacterDataSO characterData;

            foreach (var card in _characterCards)
            {
                characterData = _characterDataProvider.GetRandomCharacterData();

                if (characterData)
                {
                    card.Refresh(characterData);
                }
            }
        }

        #endregion
    }
}
