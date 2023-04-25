//-----------------------------------------------------------------------
// File name: CharacterSpawnerSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Characters
{
    [CreateAssetMenu(fileName = "CharacterSpawner", menuName = "CyberTitans/Characters/Character Spawner")]
    public class CharacterSpawnerSO : DescriptionBaseSO
    {
        #region Methods

        public Character SpawnCharacter(CharacterDataSO characterData, Transform parent = null)
        {
            return SpawnCharacter(characterData, Vector3.zero, Quaternion.identity, parent);
        }

        public Character SpawnCharacter(CharacterDataSO characterData,
                                        Vector3 position,
                                        Quaternion rotation,
                                        Transform parent = null)
        {
            Character characterPrefab = characterData?.CharacterPrefab;
            Character character = null;

            if (characterPrefab)
            {
                character = Instantiate(characterPrefab, position, rotation, parent);
                character.Initialize(characterData);
            }

            return character;
        }

        #endregion
    }
}
