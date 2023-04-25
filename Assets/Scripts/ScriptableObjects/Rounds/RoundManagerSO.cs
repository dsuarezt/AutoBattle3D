//-----------------------------------------------------------------------
// File name: RoundManagerSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitLab.CyberTitans.Shared;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Rounds
{
    [CreateAssetMenu(fileName = "RoundManager", menuName = "CyberTitans/Rounds/Round Manager")]
    public class RoundManagerSO : DescriptionBaseSO, IScriptableObjectResettable
    {
        #region Fields

        [SerializeField] private RoundInitialSettingsSO _initialSettings = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private IntEventChannelSO _onPreparationTimeChangedChannel = default;

        private CountdownTimer _countdownTimer;

        #endregion

        #region Properties

        // TODO:
        public bool IsTheLastRound => false;

        #endregion

        #region Methods

        public void Initialize()
        {
            _countdownTimer = new CountdownTimer();
            _countdownTimer.OnValueChangedEvent += OnTimerValueChanged;
        }

        public async UniTask StartPreparationPhaseAsync(CancellationToken cancellationToken)
        {
            await _countdownTimer.StartAsync(_initialSettings.PreparationTime, cancellationToken);
        }

        public async UniTask FinishPreparationPhaseAsync(CancellationToken cancellationToken)
        {
            // TODO:
            await UniTask.Delay(TimeSpan.FromSeconds(5));
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
        }

        private void OnTimerValueChanged(int value)
        {
            _onPreparationTimeChangedChannel?.RaiseEvent(this, value);
        }

        #endregion
    }
}
