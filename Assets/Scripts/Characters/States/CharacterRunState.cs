//-----------------------------------------------------------------------
// File name: CharacterRunState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 27, 2023
//-----------------------------------------------------------------------

using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LitLab.CyberTitans.Characters
{
    public class CharacterRunState : CharacterStateBase
    {
        #region Fields

        private CancellationTokenSource _cancellationTokenSource;
        private float _sqrMinDistanceToAttack;
        private float _movementSpeed;

        #endregion

        #region Constructors

        public CharacterRunState(Character character) : base(character)
        {
            float minDistanceToAttack = _character.CharacterData.MinDistanceToAttak;
            _sqrMinDistanceToAttack = minDistanceToAttack * minDistanceToAttack;
            _movementSpeed = _character.CharacterData.MovementSpeed;
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            GoToTargetEnemy(_cancellationTokenSource.Token).Forget();
        }

        public override void Reset()
        {
            _character.ChangeToIdleAnimatorState();
            _character.ChangeState(nameof(CharacterIdleState));
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;
        }

        public override void Combat()
        {
        }

        public override void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
        }

        private async UniTask GoToTargetEnemy(CancellationToken cancellationToken)
        {
            Vector3 distanceVector;
            float sqrDistance;

            while (!cancellationToken.IsCancellationRequested)
            {
                if (!_character.TargetEnemy)
                {
                    _character.ChangeToIdleAnimatorState();
                    _character.ChangeState(nameof(CharacterIdleState));

                    return;
                }

                distanceVector = CalculateDistanceVector(_character, _character.TargetEnemy);
                sqrDistance = distanceVector.sqrMagnitude;

                if (sqrDistance <= _sqrMinDistanceToAttack)
                {
                    _character.ChangeToAttackAnimatorState();
                    _character.ChangeState(nameof(CharacterAttackState));

                    return;
                }

                _character.transform.Translate(distanceVector.normalized * _movementSpeed * Time.deltaTime, Space.World);

                await UniTask.Yield(cancellationToken);
            }
        }

        private Vector3 CalculateDistanceVector(Character from, Character to)
        {
            return to.transform.position - from.transform.position;
        }

        #endregion
    }
}
