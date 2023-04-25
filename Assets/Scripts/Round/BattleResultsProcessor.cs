//-----------------------------------------------------------------------
// File name: BattleResultsProcessor.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace LitLab.CyberTitans.Rounds
{
    public class BattleResultsProcessor
    {
        #region Methods

        public int GetWinStreak(IList<BattleResult> battleResults)
        {
            int winStreak = 0;

            foreach (var result in battleResults)
            {
                if (result == BattleResult.Lost && winStreak > 0) return 0;

                if (result == BattleResult.Won) winStreak++;
            }

            return winStreak;
        }

        #endregion
    }
}
