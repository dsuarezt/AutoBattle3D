//-----------------------------------------------------------------------
// File name: RaritySO.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 21, 2023
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Characters
{
    [CreateAssetMenu(fileName = "Rarity", menuName = "Cyber Titans/Characters/Rarity")]
    public class RaritySO : DescriptionBaseSO, IEquatable<RaritySO>
    {
        #region Fields

        [SerializeField] private string _name = default;
        [SerializeField] private int _cost = default;

        #endregion

        #region Properties

        public string Name => _name;
        public int Cost => _cost;

        #endregion

        #region Operators

        public static bool operator ==(RaritySO r1, RaritySO r2)
        {
            return r1.Name == r2.Name || r1.Cost == r2.Cost;
        }

        public static bool operator !=(RaritySO r1, RaritySO r2)
        {
            return r1.Name != r2.Name || r1.Cost != r2.Cost;
        }

        #endregion

        #region Methods

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            return Equals(obj as RaritySO);
        }

        public bool Equals(RaritySO other)
        {
            return other != null &&
                   base.Equals(other) &&
                   Name == other.Name &&
                   Cost == other.Cost;
        }

        public override int GetHashCode()
        {
            int hashCode = -1570868908;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Cost.GetHashCode();
            return hashCode;
        }

        #endregion
    }
}
