using System.ComponentModel;

namespace Pomodoro.UWP.Services
{
    public interface ISettings : INotifyPropertyChanged
    {
        bool IsShowingSettings { get; set; }
        int RestingDuration { get; set; }
        int WorkingDuration { get; set; }

        void SetDurations(int workingDuration, int restingDuration);
    }
}