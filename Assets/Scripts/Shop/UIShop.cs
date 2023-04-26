//-----------------------------------------------------------------------
// File name: UIShop.cs
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
    public class UIShop : MonoBehaviour
    {
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private ShopSettingsSO _initialSettings = default;
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;
        [SerializeField] private CharacterDataProviderSO _characterDataProvider = default;

        [Header(AttributeConstants.UI_ELEMENTS)]
        [SerializeField] private TMP_Text _refreshCostText = default;
        [SerializeField] private UICard[] _cards = default;

        [Header(AttributeConstants.INVENTORY_SYSTEM)]
        [SerializeField] private InventoryController _inventory = default;

        private int _shopRefreshCost;
        private CharacterDataSO _temCharacterData;

        #endregion

        #region Engine Methods

        private void Start()
        {
            _shopRefreshCost = _initialSettings.ShopRefreshCost;
            _refreshCostText.text = _shopRefreshCost.ToString();
            GenerateNewCards();
        }

        #endregion

        #region Methods

        public void Refresh() // It's called from a UI Button.
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

        public void BuyCharacter(CharacterDataSO characterData, UICard card)
        {
            if (_inventory.AnyEmptySlot)
            {
                bool wasPaymentSuccessful = _levelEconomyManager.TryMakePayment(characterData.Cost);

                if (wasPaymentSuccessful)
                {
                    _inventory.AddCharacter(characterData);
                    GLDebug.Log($"Putting the character {characterData.CharacterName} on the inventory.", Color.green);

                    GenerateNewCard(card);
                }
                else
                {
                    GLDebug.Log("There is not enough gold to buy a character.", Color.red);
                }
            }
            else
            {
                GLDebug.Log("There are no empty slots in the inventory.", Color.red);
            }
        }

        private void GenerateNewCards()
        {
            foreach (var card in _cards)
            {
                GenerateNewCard(card);
            }
        }

        private void GenerateNewCard(UICard card)
        {
            _temCharacterData = _characterDataProvider.GetRandomCharacterData();

            if (_temCharacterData)
            {
                card.Refresh(_temCharacterData);
                _temCharacterData = null;
            }
        }

        #endregion
    }
}

