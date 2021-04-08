using Pomodoro.UWP.Observers;
using System;
using System.ComponentModel;

namespace Pomodoro.UWP.Services
{
    public interface IPomodor
    {
        TimeSpan Duration { get; }
        void RegisterObserver(IPomodorObserver observer);
        void UnRegisterObserver(IPomodorObserver observer);

        void ResetTimer();
        void StartTimer();
        void StopTimer();
    }
}