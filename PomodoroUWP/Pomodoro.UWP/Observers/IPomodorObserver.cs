using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.UWP.Observers
{
    public interface IPomodorObserver
    {
        void NotifyDurationChanged(TimeSpan duration);
    }
}
