//-----------------------------------------------------------------------
// File name: CharacterFindEnemyState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 27, 2023
//-----------------------------------------------------------------------

namespace LitLab.CyberTitans.Characters
{
    public class CharacterFindEnemyState : CharacterStateBase
	{
        #region Constructors

        public CharacterFindEnemyState(Character character) : base(character)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            if (_character.TargetEnemy)
            {
                _character.ChangeToRunAnimatorState();
                _character.ChangeState(nameof(CharacterRunState));
            }
        }

        public override void Reset()
        {
            _character.ChangeToIdleAnimatorState();
            _character.ChangeState(nameof(CharacterIdleState));
        }

        public override void Combat()
        {
        }

        public override void OnDestroy()
        {
        }

        #endregion
    }
}
