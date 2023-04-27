//-----------------------------------------------------------------------
// File name: CharacterIdleState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 27, 2023
//-----------------------------------------------------------------------

namespace LitLab.CyberTitans.Characters
{
    public class CharacterIdleState : CharacterStateBase
    {
        #region Constructors

        public CharacterIdleState(Character character) : base(character)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
        }

        public override void Reset()
        {
        }

        public override void Combat()
        {
             _character.ChangeState(nameof(CharacterFindEnemyState));
        }

        public override void OnDestroy()
        {
        }

        #endregion
    }
}
