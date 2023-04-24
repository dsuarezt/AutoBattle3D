//-----------------------------------------------------------------------
// File name: CountdownTimer.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 25, 2023
//-----------------------------------------------------------------------

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace LitLab.CyberTitans.Shared
{
    public class CountdownTimer
    {
        #region Events

        public event Action<int> OnValueChangedEvent;

        #endregion

        #region Methods

        public async UniTask StartAsync(int timeInSeconds, CancellationToken cancellationToken)
        {
            OnValueChangedEvent?.Invoke(timeInSeconds);

            while (!cancellationToken.IsCancellationRequested && timeInSeconds > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);
                OnValueChangedEvent?.Invoke(--timeInSeconds);
            }
        }

        #endregion
    }
}
