//-----------------------------------------------------------------------
// File name: LevelController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using System.Threading;
using Cysharp.Threading.Tasks;
using LitLab.CyberTitans.Battlefield;
using LitLab.CyberTitans.Rounds;
using LitLab.CyberTitans.Shared;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LitLab.CyberTitans.Level
{
    public class LevelController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private BattlefieldController _battlefieldController = default;
        [SerializeField] private EnemyBattlefieldController _enemyBattlefieldController = default;
        [SerializeField] private GameObject _battlefieldInputBlocker = default;

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;
        [SerializeField] private SelectionControllerSO _selectionController = default;
        [SerializeField] private RoundManagerSO _roundManagerSO = default;

        [Header(AttributeConstants.UI_ELEMENTS)]
        [SerializeField] private GameObject _levelUI = default;
        [SerializeField] private GameObject _preparationPhaseMessage = default;
        [SerializeField] private GameObject _combatPhaseMessage = default;
        [SerializeField] private GameObject _winMenu = default;
        [SerializeField] private GameObject _lostMenu = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private VoidEventChannelSO _onPreparationPhaseStartedChannel = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private VoidEventChannelSO _onPreparationPhaseFinishedChannel = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private VoidEventChannelSO _onCombatPhaseStartedChannel = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private VoidEventChannelSO _onCombatPhaseFinishedChannel = default;

        private ILevelState _currentState;
        private IStateConfiguration _stateConfiguration;

        #endregion

        #region Properties

        public GameObject LevelUI => _levelUI;
        public GameObject PreparationPhaseMessage => _preparationPhaseMessage;
        public GameObject CombatPhaseMessage => _combatPhaseMessage;
        public GameObject WinMenu => _winMenu;
        public GameObject LostMenu => _lostMenu;
        public LevelEconomyManagerSO LevelEconomyManager => _levelEconomyManager;
        public BattlefieldController BattlefieldController => _battlefieldController;
        public EnemyBattlefieldController EnemyBattlefieldController => _enemyBattlefieldController;
        public RoundManagerSO RoundManager => _roundManagerSO;
        public VoidEventChannelSO OnPreparationPhaseStartedChannel => _onPreparationPhaseStartedChannel;
        public VoidEventChannelSO OnPreparationPhaseFinishedChannel => _onPreparationPhaseFinishedChannel;
        public VoidEventChannelSO OnCombatPhaseStartedChannel => _onCombatPhaseStartedChannel;
        public VoidEventChannelSO OnCombatPhaseFinishedChannel => _onCombatPhaseFinishedChannel;

        #endregion

        #region Engine Methods

        private void Awake()
        {
            ConfigureStates();
        }

        private void OnDestroy()
        {
            ResetLevel();
        }

        #endregion

        #region Methods

        public void StartLevel()
        {
            ChangeState(nameof(LevelInitialState));
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(0);
        }

        public void ChangeState(string newStateName)
        {
            _currentState = _stateConfiguration.GetState(newStateName) as ILevelState;
            _currentState?.Enter();
        }

        public void Initialize()
        {
            _levelEconomyManager?.Initialize();
            _selectionController?.Initialize();
            _roundManagerSO?.Initialize();
        }

        public void ActiveBattlefieldInputBlocker(bool value)
        {
            _battlefieldInputBlocker.SetActive(value);
        }

        public CancellationToken GetCancellationToken()
        {
            return this.GetCancellationTokenOnDestroy();
        }

        private void ConfigureStates()
        {
            _stateConfiguration = new StateConfiguration();
            _stateConfiguration.AddState(nameof(LevelInitialState), new LevelInitialState(this));
            _stateConfiguration.AddState(nameof(LevelPreparationStartedState), new LevelPreparationStartedState(this));
            _stateConfiguration.AddState(nameof(LevelPreparationFinishedState), new LevelPreparationFinishedState(this));
            _stateConfiguration.AddState(nameof(LevelCombatStartedState), new LevelCombatStartedState(this));
            _stateConfiguration.AddState(nameof(LevelCombatFinishedState), new LevelCombatFinishedState(this));
            _stateConfiguration.AddState(nameof(LevelFinishedState), new LevelFinishedState(this));
        }

        private void ResetLevel()
        {
            _levelEconomyManager?.ResetLevelEconomy();
            _selectionController?.ResetSelection();
            _roundManagerSO?.ResetRound();
        }

        #endregion
    }
}
