//-----------------------------------------------------------------------
// File name: RoundManagerSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitLab.CyberTitans.Level;
using LitLab.CyberTitans.Shared;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Rounds
{
    [CreateAssetMenu(fileName = "RoundManager", menuName = "CyberTitans/Rounds/Round Manager")]
    public class RoundManagerSO : DescriptionBaseSO, IScriptableObjectResettable
    {
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private RoundSettingsSO _initialSettings = default;
        [SerializeField] private BattleDirectorSO _battleDirector = default;
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private IntEventChannelSO _onPreparationTimeChangedChannel = default;

        private CountdownTimer _countdownTimer;
        private IList<BattleResult> _battleResults;

        #endregion

        #region Properties

        public bool IsTheLastRound => _battleResults.Count == _initialSettings.RoundAmount;
        public BattleResult? LastBattleResult => _battleResults?.LastOrDefault();

        #endregion

        #region Methods

        public void Initialize()
        {
            _countdownTimer = new CountdownTimer();
            _countdownTimer.OnValueChangedEvent += OnTimerValueChanged;
            _battleResults = new List<BattleResult>();
        }

        public async UniTask StartPreparationPhaseAsync(CancellationToken cancellationToken)
        {
            await _countdownTimer.StartAsync(_initialSettings.PreparationTime, cancellationToken);
        }

        public async UniTask StartBattlePhaseAsync(CancellationToken cancellationToken)
        {
            BattleResult battleResult = await _battleDirector.StartNewBattle(cancellationToken);
            _battleResults.Add(battleResult);
        }

        public void Reward()
        {
            BattleResult? lastBattleResult = LastBattleResult;

            if (lastBattleResult.HasValue)
            {
                if (lastBattleResult.Value == BattleResult.Won)
                {
                    int amount = _initialSettings.GoldAmountPerBattleWon + GetGoldAmountPerWinStreak();
                    _levelEconomyManager.Deposit(amount);
                }
                else
                {
                    _levelEconomyManager.ConsumeLife();
                }
            }
        }

        public void ResetOnExitPlayMode()
        {
            Reset();
        }

        public void Reset()
        {
            if (_countdownTimer != null)
            {
                _countdownTimer.OnValueChangedEvent -= OnTimerValueChanged;
            }

            _battleResults = null;
        }

        private void OnTimerValueChanged(int value)
        {
            _onPreparationTimeChangedChannel?.RaiseEvent(this, value);
        }

        private int GetGoldAmountPerWinStreak()
        {
            if (LastBattleResult.HasValue && LastBattleResult.Value == BattleResult.Lost) return 0;

            int winStreak = 0;
            int length = _battleResults.Count - 1;

            for (int i = 0; i < length; i++)
            {
                if (_battleResults[i] == BattleResult.Lost && winStreak > 0) return 0;

                if (_battleResults[i] == BattleResult.Won) winStreak++;
            }

            return winStreak * _initialSettings.GoldAmountPerWinStreak;
        }

        #endregion
    }
}
