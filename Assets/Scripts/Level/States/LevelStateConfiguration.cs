//-----------------------------------------------------------------------
// File name: LevelStateConfiguration.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 24, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace LitLab.CyberTitans.Level
{
    public class LevelStateConfiguration: ILevelStateConfiguration
    {
        #region Fields

        private IDictionary<string, ILevelState> _states;

        #endregion

        #region Constructors

        public LevelStateConfiguration()
        {
            _states = new Dictionary<string, ILevelState>();
        }

        #endregion

        #region Methods

        public void AddState(string stateName, ILevelState state)
        {
            if (!_states.ContainsKey(stateName))
            {
                _states[stateName] = state;
            }
        }

        public ILevelState GetState(string stateName)
        {
            if (_states.ContainsKey(stateName))
            {
                return _states[stateName];
            }

            return null;
        }

        #endregion
    }
}
