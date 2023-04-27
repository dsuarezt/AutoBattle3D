//-----------------------------------------------------------------------
// File name: CharacterAttackState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 27, 2023
//-----------------------------------------------------------------------

namespace LitLab.CyberTitans.Characters
{
    public class CharacterAttackState : CharacterStateBase
    {
        #region Constructors

        public CharacterAttackState(Character character) : base(character)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
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
