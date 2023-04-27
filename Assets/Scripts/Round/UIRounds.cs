//-----------------------------------------------------------------------
// File name: UIRounds.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 26, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;
using LitLab.CyberTitans.Shared;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Rounds
{
    public class UIRounds : MonoBehaviour
    {
        #region Fields

        [SerializeField] private UIRoundItem _roundItemPrefab = default;

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private RoundSettingsSO _roundSettings = default;
        [SerializeField] private RoundManagerSO _roundManager = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private VoidEventChannelSO _onPreparationPhaseStartedChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private VoidEventChannelSO _onCombatPhaseFinishedChannel = default;

        private IList<UIRoundItem> _roundItems = new List<UIRoundItem>();
        private int _index;

        #endregion

        #region Engine Methods

        private void Awake()
        {
            RegisterListeners();
        }

        private void Start()
        {
            GenerateItems();
        }

        private void OnDestroy()
        {
            UnregisterListeners();
        }

        #endregion

        #region Methods

        private void RegisterListeners()
        {
            _onPreparationPhaseStartedChannel.OnEventRaised += OnPreparationPhaseStarted;
            _onCombatPhaseFinishedChannel.OnEventRaised += OnCombatPhaseFinished;
        }

        private void UnregisterListeners()
        {
            _onPreparationPhaseStartedChannel.OnEventRaised -= OnPreparationPhaseStarted;
            _onCombatPhaseFinishedChannel.OnEventRaised -= OnCombatPhaseFinished;
        }

        private void OnPreparationPhaseStarted(object sender)
        {
            if (_index > 0)
            {
                _roundItems[_index - 1].ActiveBackground(false);
            }

            _roundItems[_index].ActiveBackground(true);
        }

        private void OnCombatPhaseFinished(object sender)
        {
            CombatResult combatResult = _roundManager.LastCombatResult;

            if (combatResult == CombatResult.Won)
            {
                _roundItems[_index].ChangeToWinState();
            }
            else
            {
                _roundItems[_index].ChangeToLostState();
            }

            _index++;
        }

        private void GenerateItems()
        {
            int length = _roundSettings.RoundAmount;
            UIRoundItem roundItem;
            Transform thisTransform = transform;

            for (int i = 0; i < length; i++)
            {
                roundItem = Instantiate(_roundItemPrefab, thisTransform);
                _roundItems.Add(roundItem);
            }
        }

        #endregion
    }
}
