//-----------------------------------------------------------------------
// File name: StatsPanel.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using GivingLife.Debugging;
using LitLab.CyberTitans.Shared;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace LitLab.CyberTitans.Level
{
    public class StatsPanel : MonoBehaviour
    {
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private LevelEconomyInitialSettingsSO _initialSettings = default;
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;

        [Header(AttributeConstants.UI_ELEMENTS)]
        [SerializeField] private TMP_Text _goldAmountText = default;
        [SerializeField] private TMP_Text _livesAmountText = default;
        [SerializeField] private TMP_Text _playerLevelText = default;
        [SerializeField] private TMP_Text _upgradeCostText = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private IntEventChannelSO _onGoldAmountChangedChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private IntEventChannelSO _onLivesAmountChangedChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private IntEventChannelSO _onPlayerLevelChangedChannel = default;

        #endregion

        #region Engine Methods

        private void Start()
        {
            RegisterListeners();
            InitializeUI();
        }

        private void OnDestroy()
        {
            UnregisterListeners();
        }

        #endregion

        #region Methods

        public void UpgradePlayerLevel() // It's called from a UI Button.
        {
            int upgradeCost = _initialSettings.PlayerLevelUpgradeCost;
            bool success = _levelEconomyManager.TryMakePayment(upgradeCost);

            if (success)
            {
                GLDebug.Log("Upgrading the player level.", Color.green);
                _levelEconomyManager.UpgradePlayerLevel();
            }
            else
            {
                GLDebug.Log("There is not enough gold to upgrade the player level.", Color.red);
            }
        }

        private void RegisterListeners()
        {
            _onGoldAmountChangedChannel.OnEventRaised += OnGoldAmountChanged;
            _onLivesAmountChangedChannel.OnEventRaised += OnLivesAmountChanged;
            _onPlayerLevelChangedChannel.OnEventRaised += OnPlayerLevelChanged;
        }

        private void UnregisterListeners()
        {
            _onGoldAmountChangedChannel.OnEventRaised -= OnGoldAmountChanged;
            _onLivesAmountChangedChannel.OnEventRaised -= OnLivesAmountChanged;
            _onPlayerLevelChangedChannel.OnEventRaised -= OnPlayerLevelChanged;
        }

        private void OnGoldAmountChanged(object sender, int value)
        {
            _goldAmountText.text = value.ToString();
        }

        private void OnLivesAmountChanged(object sender, int value)
        {
            _livesAmountText.text = value.ToString();
        }

        private void OnPlayerLevelChanged(object sender, int value)
        {
            _playerLevelText.text = value.ToString();
        }

        private void InitializeUI()
        {
            _goldAmountText.text = _initialSettings.GoldAmount.ToString();
            _livesAmountText.text = _initialSettings.LivesAmount.ToString();
            _playerLevelText.text = _initialSettings.PlayerLevel.ToString();
            _upgradeCostText.text = _initialSettings.PlayerLevelUpgradeCost.ToString();
        }

        #endregion
    }
}
