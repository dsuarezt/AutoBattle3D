//-----------------------------------------------------------------------
// File name: CharacterCard.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Shared;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LitLab.CyberTitans.Shop
{
    public class CharacterCard : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Shop _shop = default;

        [Header(AttributeConstants.UI_ELEMENTS)]
        [SerializeField] private GameObject _cardContent = default;
        [SerializeField] private TMP_Text _costText = default;
        [SerializeField] private TMP_Text _characterNameText = default;
        [SerializeField] private Image _characterImage = default;

        private CharacterDataSO _characterData;

        #endregion

        #region Methods

        public void Refresh(CharacterDataSO characterData)
        {
            _characterData = characterData;

            if (_characterData)
            {
                _costText.text = _characterData.Cost.ToString();
                _characterNameText.text = _characterData.CharacterName.ToString();
                _characterImage.sprite = _characterData.CardImage;
                ActivateContent(true);
            }
        }

        public void BuyCharacter()
        {
            if (_shop.TryBuyCharacter(_characterData))
            {
                ActivateContent(false);
                _characterData = null;
            }
        }

        private void ActivateContent(bool value)
        {
            _cardContent?.SetActive(value);
        }

        #endregion
    }
}
