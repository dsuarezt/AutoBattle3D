//-----------------------------------------------------------------------
// File name: BattlefieldController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Level;
using LitLab.CyberTitans.Shared;
using LitLab.CyberTitans.Slots;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Battlefield
{
    public class BattlefieldController : SlotsControllerBase
    {
        #region Fields

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private VoidEventChannelSO _onPreparationPhaseStartedChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private VoidEventChannelSO _onPreparationPhaseFinishedChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private VoidEventChannelSO _onCombatPhaseStartedChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private VoidEventChannelSO _onCombatPhaseFinishedChannel = default;

        [BoxGroup(AttributeConstants.BROADCASTING_ON)]
        [SerializeField] private VoidEventChannelSO _onCancelSelectionChannel = default;

        [Space(5)]
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;
        [SerializeField] private EnemyBattlefieldController _enemyBattlefield = default;

        private bool _isPreparationPhase;
        private Character _characterSelected;
        private CharacterFinder _characterFinder;

        #endregion

        #region Engine Methods

        protected override void Awake()
        {
            base.Awake();
            _characterFinder = new CharacterFinder(_enemyBattlefield.Enemies);
        }

        #endregion

        #region Methods

        public override bool CanReceiveACharacter(Character character)
        {
            return Contains(character) || _characters.Count < _levelEconomyManager.PlayerLevel;
        }

        protected override void RegisterListeners()
        {
            base.RegisterListeners();
            _onPreparationPhaseStartedChannel.OnEventRaised += OnPreparationPhaseStarted;
            _onPreparationPhaseFinishedChannel.OnEventRaised += OnPreparationPhaseFinished;
            _onCombatPhaseStartedChannel.OnEventRaised += OnCombatPhaseStarted;
            _onCombatPhaseFinishedChannel.OnEventRaised += OnCombatPhaseFinishedChannel;
        }

        protected override void UnregisterListeners()
        {
            base.UnregisterListeners();
            _onPreparationPhaseStartedChannel.OnEventRaised -= OnPreparationPhaseStarted;
            _onPreparationPhaseFinishedChannel.OnEventRaised -= OnPreparationPhaseFinished;
            _onCombatPhaseStartedChannel.OnEventRaised -= OnCombatPhaseStarted;
            _onCombatPhaseFinishedChannel.OnEventRaised -= OnCombatPhaseFinishedChannel;
        }

        protected override void OnSelectCharacter(object sender, Slot slot)
        {
            _characterSelected = slot.Character;

            if (_isPreparationPhase)
            {
                ActivateSlots(true);
                AllowCharacterSelection(false);
            }
        }

        protected override void OnDeselectCharacter(object sender, Slot slot)
        {
            _characterSelected = null;

            if (_isPreparationPhase)
            {
                ActivateSlots(false);
                AllowCharacterSelection(true);
            }
        }

        private bool Contains(Character character)
        {
            return _characters.Contains(character);
        }

        private void OnPreparationPhaseStarted(object sender)
        {
            _isPreparationPhase = true;
            AllowCharacterSelection(true);

            if (_characterSelected)
            {
                ActivateSlots(true);
            }
            else
            {
                AllowCharacterSelection(true);
            }
        }

        private void OnPreparationPhaseFinished(object sender)
        {
            _isPreparationPhase = false;
            ActivateSlots(false);
            AllowCharacterSelection(false);

            if (_characterSelected && _characters.Contains(_characterSelected))
            {
                // If the preparation has finished and a character is being selected from the battlefield,
                // it is necessary to deselect it to return it to its slot.
                _onCancelSelectionChannel?.RaiseEvent();
            }
        }

        private void OnCombatPhaseStarted(object sender)
        {
            foreach (var character in _characters)
            {
                character.Combat(_characterFinder);
            }
        }

        private void OnCombatPhaseFinishedChannel(object sender)
        {
            ResetCharacters();
        }

        private void ResetCharacters()
        {
            foreach (var selector in _characterSelectors)
            {
                selector.Slot.ResetCharacter();
            }
        }

        #endregion
    }
}
