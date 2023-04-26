//-----------------------------------------------------------------------
// File name: UIRoundItem.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 26, 2023
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace LitLab.CyberTitans.Rounds
{
    public class UIRoundItem : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _iconImage = default;
        [SerializeField] private Image _backgroundImage = default;
        [SerializeField] private Color _defaultIconColor = default;
        [SerializeField] private Color _winIconColor = default;
        [SerializeField] private Color _lostIconColor = default;

        #endregion

        #region Engine Methods

        private void Awake()
        {
            ActiveBackground(false);
            _iconImage.color = _defaultIconColor;
        }

        #endregion

        #region Methods

        public void ActiveBackground(bool value)
        {
            _backgroundImage.enabled = value;
        }

        public void ChangeToWinState()
        {
            _iconImage.color = _winIconColor;
        }

        public void ChangeToLostState()
        {
            _iconImage.color = _lostIconColor;
        }

        #endregion
    }
}
