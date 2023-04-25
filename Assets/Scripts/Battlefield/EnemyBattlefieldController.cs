//-----------------------------------------------------------------------
// File name: EnemyBattlefieldController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Battlefield
{
    public class EnemyBattlefieldController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _spownPoints = default;

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private CharacterSpawnerSO _characterSpawner = default;
        [SerializeField] private CharacterDataProviderSO _characterDataProvider = default;

        private IList<Character> _characters = new List<Character>();

        #endregion

        #region Properties



        #endregion

        #region Engine Methods



        #endregion

        #region Methods

        public async UniTask GenerateEnemiesAsync()
        {

        }

        #endregion
    }
}
