//-----------------------------------------------------------------------
// File name: CharacterDataSO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 21, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Characters
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "CyberTitans/Characters/Character Data")]
    public class CharacterDataSO : DescriptionBaseSO
    {
        #region Fields

        [SerializeField] private string _name = default;
        [SerializeField] private GameObject _characterPrefab = default;
        [SerializeField] private RaritySO _rarity;
        [SerializeField] private Texture _cardImage = default;
        [SerializeField] private int _health = default;
        [SerializeField] private int _damage = default;

        #endregion

        #region Properties
         
        public string Name => _name;
        public GameObject CharacterPrefab => _characterPrefab;
        public RaritySO Rarity => _rarity;
        public int Cost => _rarity.Cost;
        public Texture CardImage => _cardImage;
        public int Health => _health;
        public int Damage => _damage;

        #endregion
    }
}
