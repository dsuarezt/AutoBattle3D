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

        [SerializeField] private string _characterName = default;
        [SerializeField] private Character _characterPrefab = default;
        [SerializeField] private RaritySO _rarity;
        [SerializeField] private Sprite _cardImage = default;
        [SerializeField] private int _health = 10;
        [SerializeField] private int _damage = 3;

        #endregion

        #region Properties
         
        public string CharacterName => _characterName;
        public Character CharacterPrefab => _characterPrefab;
        public RaritySO Rarity => _rarity;
        public int Cost => _rarity.Cost;
        public Sprite CardImage => _cardImage;
        public int Health => _health;
        public int Damage => _damage;

        #endregion
    }
}
