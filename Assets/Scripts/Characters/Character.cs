//-----------------------------------------------------------------------
// File name: Character.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Characters
{
    public class Character : MonoBehaviour
    {
        #region Fields

        private CharacterDataSO _characterData;
        private IStateConfiguration _stateConfiguration;
        private ICharacterState _currentState;
        private Character _targetEnemy;

        private Animator _animator;
        private int _idleStateId;
        private int _chargeStateId;
        private int _attackStateId;

        #endregion

        #region Properties

        public CharacterDataSO CharacterData => _characterData;
        public CharacterFinder CharacterFinder { get; private set; }
        public Character TargetEnemy => _targetEnemy ?? (_targetEnemy = CharacterFinder?.FindNearestCharacter(this));

        #endregion

        #region Engine Methods

        private void OnDestroy()
        {
            _currentState?.OnDestroy();
        }

        #endregion

        #region Methods

        public void Initialize(CharacterDataSO characterData)
        {
            _characterData = characterData;
            name = characterData.CharacterName;
            _animator = GetComponent<Animator>();
            GetAnimatorStateIds();
            ConfigureStates();
            ChangeState(nameof(CharacterIdleState));
        }

        public void Combat(CharacterFinder characterFinder)
        {
            CharacterFinder = characterFinder;
            _currentState?.Combat();
        }

        public void ChangeState(string newStateName)
        {
            _currentState = _stateConfiguration.GetState(newStateName) as ICharacterState;
            _currentState?.Enter();
        }

        public void ChangeToIdleAnimatorState()
        {
            _animator.SetTrigger(_idleStateId);
        }

        public void ChangeToChargeAnimatorState()
        {
            _animator.SetTrigger(_chargeStateId);
        }

        public void ChangeToAttackAnimatorState()
        {
            _animator.SetTrigger(_attackStateId);
        }

        public void ResetCharacter()
        {
            _targetEnemy = null;
            _currentState.Reset();
        }

        private void ConfigureStates()
        {
            _stateConfiguration = new StateConfiguration();
            _stateConfiguration.AddState(nameof(CharacterIdleState), new CharacterIdleState(this));
            _stateConfiguration.AddState(nameof(CharacterFindEnemyState), new CharacterFindEnemyState(this));
            _stateConfiguration.AddState(nameof(CharacterChargeState), new CharacterChargeState(this));
            _stateConfiguration.AddState(nameof(CharacterAttackState), new CharacterAttackState(this));
        }

        private void GetAnimatorStateIds()
        {
            _idleStateId = Animator.StringToHash("idle");
            _chargeStateId = Animator.StringToHash("charge");
            _attackStateId = Animator.StringToHash("attack");
        }

        #endregion
    }
}
