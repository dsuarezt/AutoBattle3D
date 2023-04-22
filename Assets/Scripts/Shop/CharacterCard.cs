//-----------------------------------------------------------------------
// File name: CharacterCard.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LitLab.CyberTitans.Shop
{
    public class CharacterCard : MonoBehaviour
    {
        #region Constants



        #endregion

        #region Fields

        [SerializeField] private GameObject _cardContent = default;
        [SerializeField] private TMP_Text _costText = default;
        [SerializeField] private TMP_Text _characterNameText = default;
        [SerializeField] private Image _characterImage = default;

        private CharacterDataSO _characterData;

        #endregion

        #region Properties



        #endregion

        #region Engine Methods



        #endregion

        #region Methods

        public void Refresh(CharacterDataSO characterData)
        {
            _characterData = characterData;
            _costText.text = _characterData.Cost.ToString();
            _characterNameText.text = _characterData.CharacterName.ToString();
            _characterImage.sprite = _characterData.CardImage;
            ActivateContent(true);
        }

        public void BuyCharacter()
        {
            // TODO: Check if there is enough gold and if there are empty slots in the inventory.
            if (true)
            {
                // TODO: Put the character on the inventory.
                ActivateContent(false);
                _characterData = null;
            }
        }

        private void ActivateContent(bool value)
        {
            _cardContent.SetActive(value);
        }

        #endregion
    }
}
