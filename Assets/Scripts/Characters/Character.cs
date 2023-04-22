//-----------------------------------------------------------------------
// File name: Character.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using UnityEngine;

namespace LitLab.CyberTitans.Characters
{
    public class Character : MonoBehaviour
    {
        #region Constants



        #endregion

        #region Fields

        private CharacterDataSO _characterData;

        #endregion

        #region Properties



        #endregion

        #region Engine Methods



        #endregion

        #region Methods

        public void Initialize(CharacterDataSO characterData)
        {
            _characterData = characterData;
            name = characterData.CharacterName;
        }

        #endregion
    }
}
