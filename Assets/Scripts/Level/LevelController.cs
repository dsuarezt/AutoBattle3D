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
        [SerializeField] private SelectionControllerSO _selectionController = default;

        #endregion

        #region Properties

        

        #endregion

        #region Engine Methods

        private void Awake()
        {
            _levelEconomyManager?.Initialize(_levelEconomyInitialSettings);
            _selectionController?.Initialize();
        }

        #endregion

        #region Methods

        

        #endregion
    }
}
