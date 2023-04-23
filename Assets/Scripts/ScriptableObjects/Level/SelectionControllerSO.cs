//-----------------------------------------------------------------------
// File name: SelectionControllerSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

using GivingLife.Debugging;
using LitLab.CyberTitans.Shared;
using LitLab.CyberTitans.Slots;
using NaughtyAttributes;
using UnityEngine;

namespace LitLab.CyberTitans.Level
{
    [CreateAssetMenu(fileName = "SelectionController", menuName = "CyberTitans/Level/Selection Controller")]
    public class SelectionControllerSO : DescriptionBaseSO, IScriptableObjectResettable
    {
        #region Fields

        [SerializeField] private LayerMask _interceptorLayerMask;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private SlotEventChannelSO _onSelectCharacterChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private SlotEventChannelSO _onDeselectCharacterChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private SlotEventChannelSO _onSlotMouseEnterChannel = default;

        [BoxGroup(AttributeConstants.LISTENING_TO)]
        [SerializeField] private SlotEventChannelSO _onSlotMouseExitChannel = default;

        private Slot _currentSlot;
        private Slot _targetSlot;
        private ObjectToMousePosition _objectToMousePosition;

        #endregion

        #region Methods

        public void Initialize()
        {
            GLDebug.Log($"Initializing {nameof(SelectionControllerSO)}.", Color.cyan);
            RegisterListeners();
            _objectToMousePosition = new ObjectToMousePosition(_interceptorLayerMask);
        }

        public void ResetOnExitPlayMode()
        {
            UnregisterListeners();
            _currentSlot = null;
            _targetSlot = null;
        }

        private void RegisterListeners()
        {
            _onSelectCharacterChannel.OnEventRaised += OnSelectCharacter;
            _onDeselectCharacterChannel.OnEventRaised += OnDeselectCharacter;
            _onSlotMouseEnterChannel.OnEventRaised += OnSlotMouseEnter;
            _onSlotMouseExitChannel.OnEventRaised += OnSlotMouseExit;
        }

        private void UnregisterListeners()
        {
            _onSelectCharacterChannel.OnEventRaised -= OnSelectCharacter;
            _onDeselectCharacterChannel.OnEventRaised -= OnDeselectCharacter;
            _onSlotMouseEnterChannel.OnEventRaised -= OnSlotMouseEnter;
            _onSlotMouseExitChannel.OnEventRaised -= OnSlotMouseExit;
        }

        private void OnSelectCharacter(object sender, Slot slot)
        {
            _currentSlot = slot;
            _objectToMousePosition.FollowMouse(_currentSlot.Character.gameObject);
        }

        private void OnDeselectCharacter(object sender, Slot slot)
        {
            _objectToMousePosition.Cancel();

            if (_currentSlot)
            {
                if (!_targetSlot)
                {
                    // Return the character to the center of its slot.
                    _currentSlot.ResetCharacter();
                }
                else if (_targetSlot.IsEmpty)
                {
                    if (_targetSlot.CanReceiveACharacter(_currentSlot.Character))
                    {
                        MoveCharacterToTargetSlot();
                    }
                    else
                    {
                        _currentSlot.ResetCharacter();
                    }
                }
                else if (!_targetSlot.IsEmpty)
                {
                    SwapCharacters();
                }
            }
        }

        private void MoveCharacterToTargetSlot()
        {
            var character = _currentSlot.RemoveCharacter();
            _targetSlot.AddNewCharacter(character);
        }

        private void SwapCharacters()
        {
            var currentCharacter = _currentSlot.Character;
            var targetCharacter = _targetSlot.Character;
            _currentSlot.AddNewCharacter(targetCharacter);
            _targetSlot.AddNewCharacter(currentCharacter);
        }

        private void OnSlotMouseEnter(object sender, Slot slot)
        {
            if (slot != _currentSlot) _targetSlot = slot;
        }

        private void OnSlotMouseExit(object sender, Slot slot)
        {
            _targetSlot = null;
        }

        #endregion
    }
}
