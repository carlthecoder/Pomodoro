using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PomodorTimer
{
    public class Settings : INotifyPropertyChanged
    {
        private int workingDuration = 25;
        private int restingDuration = 5;

        public int WorkingDuration
        {
            get { return workingDuration; }
            set { workingDuration = value;
                OnPropertyChanged();
            }
        }
        public int RestingDuration
        {
            get { return restingDuration; }
            set { restingDuration = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetDurations(int workingDuration, int restingDuration)
        {
            RestingDuration = restingDuration;
            WorkingDuration = workingDuration;
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
