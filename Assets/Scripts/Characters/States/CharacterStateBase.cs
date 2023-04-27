//-----------------------------------------------------------------------
// File name: CharacterStateBase.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 27, 2023
//-----------------------------------------------------------------------

namespace LitLab.CyberTitans.Characters
{
    public abstract class CharacterStateBase : ICharacterState
	{
        #region Fields

        protected readonly Character _character;

        #endregion

        #region Constructors

        protected CharacterStateBase(Character character)
        {
            _character = character;
        }

        #endregion

        #region Methods

        public abstract void Enter();

        public abstract void Reset();

        public abstract void Combat();

        public abstract void OnDestroy();

        #endregion
    }
}
