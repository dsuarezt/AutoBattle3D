//-----------------------------------------------------------------------
// File name: CharacterDataProviderSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

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
            return _characters[Random.Range(0, _characters.Length)];
        }

        #endregion
    }
}
