//-----------------------------------------------------------------------
// File name: UIPreparationTimer.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 26, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace LitLab.CyberTitans.Rounds
{
    public class UIPreparationTimer : MonoBehaviour
    {
        #region Fields

        [Header(AttributeConstants.UI_ELEMENTS)]
        [SerializeField] private TMP_Text _timerText = default;

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private RoundSettingsSO _roundSettings = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private IntEventChannelSO _onPreparationTimeChangedChannel = default;

        #endregion

        #region Engine Methods

        private void Awake()
        {
            RegisterListeners();
        }

        private void Start()
        {
            SetTimerText(_roundSettings.PreparationTime.ToString());
        }

        private void OnDestroy()
        {
            UnregisterListeners();
        }

        #endregion

        #region Methods

        private void RegisterListeners()
        {
            _onPreparationTimeChangedChannel.OnEventRaised += OnPreparationTimeChanged;
        }

        private void UnregisterListeners()
        {
            _onPreparationTimeChangedChannel.OnEventRaised -= OnPreparationTimeChanged;
        }

        private void OnPreparationTimeChanged(object sender, int value)
        {
            SetTimerText(value.ToString());
        }

        private void SetTimerText(string value)
        {
            _timerText.text = value;
        }

        #endregion
    }
}
