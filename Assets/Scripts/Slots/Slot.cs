//-----------------------------------------------------------------------
// File name: Slot.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Characters;
using UnityEngine;

namespace LitLab.CyberTitans.Level
{
    public class Slot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _base = default;
        [SerializeField] private Transform _spawnPoint = default;

        private Character _character;

        #endregion

        #region Properties

        public bool IsEmpty => _character == null;

        #endregion

        #region Methods

        public void AddCharacter(Character character)
        {
            if (character)
            {
                _character = character;
                _character.transform.SetParent(_spawnPoint);
                _character.transform.SetPositionAndRotation(_spawnPoint.position, _spawnPoint.rotation);
            }
        }

        public void ActivateBase(bool value)
        {
            _base.SetActive(value);
        }

        #endregion
    }
}
