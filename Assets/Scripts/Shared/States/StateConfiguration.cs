//-----------------------------------------------------------------------
// File name: StateConfiguration.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 24, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace LitLab.CyberTitans.Shared
{
    public class StateConfiguration: IStateConfiguration
    {
        #region Fields

        private IDictionary<string, IState> _states;

        #endregion

        #region Constructors

        public StateConfiguration()
        {
            _states = new Dictionary<string, IState>();
        }

        #endregion

        #region Methods

        public void AddState(string stateName, IState state)
        {
            if (!_states.ContainsKey(stateName))
            {
                _states[stateName] = state;
            }
        }

        public IState GetState(string stateName)
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
