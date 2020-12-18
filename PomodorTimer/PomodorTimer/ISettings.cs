using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PomodorTimer
{
    public interface ISettings: INotifyPropertyChanged
    {
        bool IsShowingSettings { get; set; }
        int WorkingDuration {get; set;}
        int RestingDuration { get; set; }
        void SetDurations(int workingDuration, int restingDuration);
    }
}
