//-----------------------------------------------------------------------
// File name: EnemyBattlefieldController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitLab.CyberTitans.Characters;
using LitLab.CyberTitans.Shared;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace LitLab.CyberTitans.Battlefield
{
    public class EnemyBattlefieldController : MonoBehaviour
    {
        #region Fields

        [Header(AttributeConstants.ENEMY_GENERATION)]
        [SerializeField] private EnemyBattlefieldSettingsSO _initialSettings = default;
        [SerializeField] private CharacterDataProviderSO _characterDataProvider = default;
        [SerializeField] private CharacterSpawnerSO _characterSpawner = default;

        [Space(5)]
        [SerializeField] private Transform[] _spownPoints = default;

        private IList<Character> _enemies = new List<Character>();

        #endregion

        #region Methods

        public async UniTask GenerateEnemiesAsync(CancellationToken cancellationToken)
        {
            int enemyAmount = GetRandomAmount();
            IList<Transform> randomSpawnPoints = GetRandomSpawnPoints(enemyAmount);

            foreach (Transform spawnPoint in randomSpawnPoints)
            {
                SpawnEnemy(spawnPoint);

                await UniTask.Delay
                (
                    delayTimeSpan: TimeSpan.FromSeconds(_initialSettings.GenerationDelay),
                    cancellationToken: cancellationToken
                );

                if (cancellationToken.IsCancellationRequested) break;
            }
        }

        public void DestroyEnemies()
        {
            foreach (var enemy in _enemies)
            {
                Destroy(enemy.gameObject);
            }

            _enemies.Clear();
        }

        private int GetRandomAmount()
        {
            return UnityEngine.Random.Range(_initialSettings.MinEnemyAmount, _initialSettings.MaxEnemyAmount + 1);
        }

        private IList<Transform> GetRandomSpawnPoints(int amount)
        {
            var result = new List<Transform>(amount);
            var spawnPointsCopy = new List<Transform>(_spownPoints);
            int index;

            for (int i = 0; i < amount; i++)
            {
                index = UnityEngine.Random.Range(0, spawnPointsCopy.Count);
                result.Add(spawnPointsCopy[index]);
                spawnPointsCopy.RemoveAt(index);
            }

            return result;
        }

        private void SpawnEnemy(Transform spawnPoint)
        {
            CharacterDataSO characterData = _characterDataProvider.GetRandomCharacterData();
            Character enemy = _characterSpawner.SpawnCharacter(characterData, spawnPoint.position, spawnPoint.rotation);
            _enemies.Add(enemy);
        }

        #endregion
    }
}
