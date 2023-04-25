//-----------------------------------------------------------------------
// File name: LevelController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using System.Threading;
using Cysharp.Threading.Tasks;
using LitLab.CyberTitans.Rounds;
using LitLab.CyberTitans.Shared;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Level
{
    public class LevelController : MonoBehaviour
    {
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private LevelEconomyInitialSettingsSO _levelEconomyInitialSettings = default;
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;
        [SerializeField] private SelectionControllerSO _selectionController = default;
        [SerializeField] private RoundManagerSO _roundManagerSO = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private VoidEventChannelSO _onPreparationPhaseStartedChannel = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private VoidEventChannelSO _onPreparationPhaseFinishedChannel = default;

        private ILevelState _currentState;
        private ILevelStateConfiguration _stateConfiguration;

        #endregion

        #region Properties

        public RoundManagerSO RoundManager => _roundManagerSO;
        public VoidEventChannelSO OnPreparationPhaseStartedChannel => _onPreparationPhaseStartedChannel;
        public VoidEventChannelSO OnPreparationPhaseFinishedChannel => _onPreparationPhaseFinishedChannel;

        #endregion

        #region Engine Methods

        private void Awake()
        {
            ConfigureStates();
        }

        private void Start()
        {
            ChangeState(nameof(LevelInitialState));
        }

        private void OnDestroy()
        {
            Reset();
        }

        #endregion

        #region Methods

        public void ChangeState(string newStateName)
        {
            _currentState = _stateConfiguration.GetState(newStateName);
            _currentState.Enter();
        }

        public void Initialize()
        {
            _levelEconomyManager?.Initialize(_levelEconomyInitialSettings);
            _selectionController?.Initialize();
            _roundManagerSO?.Initialize();
        }

        public CancellationToken GetCancellationToken()
        {
           return this.GetCancellationTokenOnDestroy();
        }

        private void ConfigureStates()
        {
            _stateConfiguration = new LevelStateConfiguration();
            _stateConfiguration.AddState(nameof(LevelInitialState), new LevelInitialState(this));
            _stateConfiguration.AddState(nameof(LevelPreparationStartedState), new LevelPreparationStartedState(this));
            _stateConfiguration.AddState(nameof(LevelPreparationFinishedState), new LevelPreparationFinishedState(this));
        }

        private void Reset()
        {
            _levelEconomyManager?.Reset();
            _selectionController?.Reset();
            _roundManagerSO?.Reset();
        }

        #endregion
    }
}
