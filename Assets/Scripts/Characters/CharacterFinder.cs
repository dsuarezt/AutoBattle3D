//-----------------------------------------------------------------------
// File name: CharacterFinder.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 27, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace LitLab.CyberTitans.Characters
{
    public class CharacterFinder
    {
        #region Fields

        private IList<Character> _characters;
        private Character _nearestCharacter;
        private float _sqrDistance;

        #endregion

        #region Constructors

        public CharacterFinder(IList<Character> characters)
        {
            _characters = characters;
        }

        #endregion

        #region Methods

        public Character FindNearestCharacter(Character character)
        {
            _nearestCharacter = null;

            if (_characters != null && _characters.Count > 0)
            {
                _nearestCharacter = _characters[0];
                _sqrDistance = CalculateSqrDistance(character, _nearestCharacter);
                int length = _characters.Count;
                float tempSqrtDistance;
                Character targetCharacter;

                for (int i = 1; i < length; i++)
                {
                    targetCharacter = _characters[i];
                    tempSqrtDistance = CalculateSqrDistance(character, targetCharacter);

                    if (tempSqrtDistance < _sqrDistance)
                    {
                        _sqrDistance = tempSqrtDistance;
                        _nearestCharacter = targetCharacter;
                    }
                }
            }

            return _nearestCharacter;
        }

        private float CalculateSqrDistance(Character from, Character to)
        {
            return (to.transform.position - from.transform.position).sqrMagnitude;
        }

        #endregion
    }
}
