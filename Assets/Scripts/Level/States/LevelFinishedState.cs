//-----------------------------------------------------------------------
// File name: LevelFinishedState.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 26, 2023
//-----------------------------------------------------------------------

using GivingLife.Debugging;
using UnityEngine;

namespace LitLab.CyberTitans.Level
{
    public class LevelFinishedState : LevelStateBase
	{
        #region Constructors

        public LevelFinishedState(LevelController levelController) : base(levelController)
        {
        }

        #endregion

        #region Methods

        public override void Enter()
        {
            GLDebug.Log("Level Finished.",Color.magenta);

            // TODO: Check lives
        }

        #endregion
    }
}
