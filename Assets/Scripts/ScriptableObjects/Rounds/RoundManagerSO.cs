//-----------------------------------------------------------------------
// File name: RoundManagerSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitLab.CyberTitans.Characters;
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
        [SerializeField] private CombatDirectorSO _combatDirector = default;
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private IntEventChannelSO _onPreparationTimeChangedChannel = default;

        private CountdownTimer _countdownTimer;
        private IList<CombatResult> _combatResults;
        private bool _isWinStreakLost;

        #endregion

        #region Properties

        public bool IsTheLastRound => _combatResults.Count == _initialSettings.RoundAmount;
        public CombatResult LastCombatResult => _combatResults.LastOrDefault();

        #endregion

        #region Methods

        public void Initialize()
        {
            _countdownTimer = new CountdownTimer();
            _combatResults = new List<CombatResult>();
            RegisterListeners();
        }

        public async UniTask StartPreparationPhaseAsync(CancellationToken cancellationToken)
        {
            await _countdownTimer.StartAsync(_initialSettings.PreparationTime, cancellationToken);
        }

        public async UniTask StartCombatPhaseAsync(IList<Character> characters,
                                                   CancellationToken cancellationToken)
        {
            CombatResult combatResult = await _combatDirector.StartNewCombatAsync(characters, cancellationToken);

            if (!cancellationToken.IsCancellationRequested) _combatResults.Add(combatResult);
        }

        public void Reward()
        {
            CombatResult lastCombatResult = LastCombatResult;

            if (lastCombatResult == CombatResult.Won)
            {
                int goldAmountPerWinStreak = !_isWinStreakLost ? GetGoldAmountPerWinStreak() : 0;
                int amount = _initialSettings.GoldAmountPerCombatWon + goldAmountPerWinStreak;
                _levelEconomyManager.Deposit(amount);
            }
            else if (lastCombatResult == CombatResult.Lost)
            {
                _levelEconomyManager.ConsumeLife();
            }
        }

        public void ResetOnExitPlayMode()
        {
            ResetRound();
        }

        public void ResetRound()
        {
            _combatResults = null;
            _isWinStreakLost = false;
            UnregisterListeners();
        }

        private void RegisterListeners()
        {
            _countdownTimer.OnValueChangedEvent += OnTimerValueChanged;
        }

        private void UnregisterListeners()
        {
            if (_countdownTimer != null)
            {
                _countdownTimer.OnValueChangedEvent -= OnTimerValueChanged;
            }
        }

        private void OnTimerValueChanged(int value)
        {
            _onPreparationTimeChangedChannel?.RaiseEvent(this, value);
        }

        private int GetGoldAmountPerWinStreak()
        {
            int winStreak = 0;
            int length = _combatResults.Count - 1;

            for (int i = 0; i < length; i++)
            {
                if (_combatResults[i] == CombatResult.Lost)
                {
                    if (winStreak > 0)
                    {
                        _isWinStreakLost = true;

                        return 0;
                    }
                }
                else
                {
                    winStreak++;
                }
            }

            return winStreak * _initialSettings.GoldAmountPerWinStreak;
        }

        #endregion
    }
}
