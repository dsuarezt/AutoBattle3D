//-----------------------------------------------------------------------
// File name: LevelController.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 22, 2023
//-----------------------------------------------------------------------

using LitLab.CyberTitans.Shared;
using UnityEngine;

namespace LitLab.CyberTitans.Level
{
    public class LevelController : MonoBehaviour
	{
        #region Fields

        [Header(AttributeConstants.SCRIPTABLE_OBJECTS)]
        [SerializeField] private LevelEconomyInitialSettingsSO _levelEconomyInitialSettings = default;
        [SerializeField] private LevelEconomyManagerSO _levelEconomyManager = default;

        #endregion

        #region Properties



        #endregion

        #region Engine Methods

        private void Awake()
        {
            InitializeLevelEconomy();
        }

        #endregion

        #region Methods

        private void InitializeLevelEconomy()
        {
            _levelEconomyManager?.Initialize(_levelEconomyInitialSettings);
        }

        #endregion
    }
}
