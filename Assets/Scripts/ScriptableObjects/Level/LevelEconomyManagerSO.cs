//-----------------------------------------------------------------------
// File name: LevelEconomyManagerSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using GivingLife.Debugging;
using LitLab.CyberTitans.Shared;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Level
{
    [CreateAssetMenu(fileName = "LevelEconomyManager", menuName = "CyberTitans/Level/Level Economy Manager")]
    public class LevelEconomyManagerSO : DescriptionBaseSO, IScriptableObjectResettable
    {
        #region Fields

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private IntEventChannelSO _onGoldAmountChangedChannel = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private IntEventChannelSO _onLivesAmountChangedChannel = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private IntEventChannelSO _onPlayerLevelChangedChannel = default;

        private LevelEconomyInitialSettingsSO _levelEconomyInitialSettings = default;
        private int _goldAmount;
        private int _livesAmount;
        private int _playerLevel;

        #endregion

        #region Properties

        public int GoldAmount
        {
            get => _goldAmount;
            private set
            {
                GLDebug.Log($"Updating {nameof(GoldAmount)} from {_goldAmount} to {value}.", Color.green);
                _goldAmount = value;
                _onGoldAmountChangedChannel.RaiseEvent(this, value);
            }
        }

        public int LivesAmount
        {
            get => _livesAmount;
            private set
            {
                GLDebug.Log($"Updating {nameof(LivesAmount)} from {_livesAmount} to {value}.", Color.green);
                _livesAmount = value;
                _onLivesAmountChangedChannel.RaiseEvent(this, value);
            }
        }

        public int PlayerLevel
        {
            get => _playerLevel;
            private set
            {
                GLDebug.Log($"Updating {nameof(PlayerLevel)} from {_playerLevel} to {value}.", Color.green);
                _playerLevel = value;
                _onPlayerLevelChangedChannel.RaiseEvent(this, value);
            }
        }

        #endregion

        #region Methods

        public void Initialize(LevelEconomyInitialSettingsSO levelEconomyInitialSettings)
        {
            _levelEconomyInitialSettings = levelEconomyInitialSettings;
            _goldAmount = _levelEconomyInitialSettings.GoldAmount;
            _livesAmount = _levelEconomyInitialSettings.LivesAmount;
            _playerLevel = _levelEconomyInitialSettings.PlayerLevel;

            GLDebug.Log($"Initializing {nameof(LevelEconomyManagerSO)}.", Color.cyan);
        }

        public bool CanMakePayment(int amount)
        {
            return amount > 0 && amount <= _goldAmount;
        }

        public bool TryMakePayment(int amount)
        {
            if (CanMakePayment(amount))
            {
                GoldAmount -= amount;

                return true;
            }

            return false;
        }

        public void Deposit(int amount)
        {
            if (amount > 0) _goldAmount += amount;
        }

        public void ConsumeLife()
        {
            if (_livesAmount > 0) LivesAmount--;
        }

        public void UpgradePlayerLevel()
        {
            PlayerLevel++;
        }

        public void ResetOnExitPlayMode()
        {
            Reset();
        }

        public void Reset()
        {
            _levelEconomyInitialSettings = null;
            _goldAmount = 0;
            _livesAmount = 0;
            _playerLevel = 0;
        }

        #endregion
    }
}
