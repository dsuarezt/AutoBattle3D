//-----------------------------------------------------------------------
// File name: RoundManagerSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

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

        [SerializeField] private int _roundAmount = default;
        [SerializeField] private int _preparationTime = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private IntEventChannelSO _onPreparationTimeChangedChannel = default;

        private CountdownTimer _countdownTimer;

        #endregion

        #region Properties

        // TODO:
        public bool IsTheLastRound => true;

        #endregion

        #region Constructors



        #endregion

        #region Engine Methods



        #endregion

        #region Methods

        public void Initialize()
        {
            _countdownTimer = new CountdownTimer();
            _countdownTimer.OnValueChangedEvent += OnTimerValueChanged;
        }

        public async UniTask StartPreparationPhaseAsync(CancellationToken cancellationToken)
        {
            await _countdownTimer.StartAsync(_preparationTime, cancellationToken);
        }

        public async UniTask StartBattlePhaseAsync(CancellationToken cancellationToken)
        {
            // TODO:
        }

        public void ResetOnExitPlayMode()
        {
            Reset();
        }

        public void Reset()
        {
            _countdownTimer.OnValueChangedEvent -= OnTimerValueChanged;
        }

        private void OnTimerValueChanged(int value)
        {
            _onPreparationTimeChangedChannel?.RaiseEvent(this, value);
        }

        #endregion
    }
}
