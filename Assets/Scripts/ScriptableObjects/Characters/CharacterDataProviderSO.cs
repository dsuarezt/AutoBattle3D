//-----------------------------------------------------------------------
// File name: CharacterDataProviderSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using GivingLife.Debugging;
using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Characters
{
    [CreateAssetMenu(fileName = "CharacterDataProvider", menuName = "CyberTitans/Characters/Character Data Provider")]
    public class CharacterDataProviderSO : DescriptionBaseSO
    {
        #region Fields

        [SerializeField] private CharacterDataSO[] _characters = default;

        #endregion

        #region Methods

        public CharacterDataSO GetRandomCharacterData()
        {
            CharacterDataSO characterData = null;

            if (_characters != null && _characters.Length > 0)
            {
                characterData = _characters[Random.Range(0, _characters.Length)];
            }

            if (!characterData)
            {
                GLDebug.LogError("Could not generate a random character because the character data is null.");
            }

            return characterData;
        }

        #endregion
    }
}
