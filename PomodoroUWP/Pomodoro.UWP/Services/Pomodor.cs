using Pomodoro.UWP.Observers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Windows.UI.Notifications;

namespace Pomodoro.UWP.Services
{
    public sealed class Pomodor : IPomodor
    {
        private const int intervalms = 1000;
        private readonly Timer _timer = new Timer(intervalms);
        private readonly ISettings _settings = Ioc.Resolve<ISettings>();
        private readonly IList<IPomodorObserver> _observers = new List<IPomodorObserver>();
        private TimeSpan _duration;

        public TimeSpan Duration
        {
            get => _duration;
            private set
            {
                _duration = value;
                NotifyObservers();
            }
        }

        public Pomodor()
        {
            _timer.Elapsed += Timer_Elapsed;

            SetDuration(setWorkingDuration: true);
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.NotifyDurationChanged(_duration);
            }
        }

        public void RegisterObserver(IPomodorObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void UnRegisterObserver(IPomodorObserver observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        public void ResetTimer()
        {
            _timer.Stop();
            SetDuration(setWorkingDuration: true);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Duration = _duration.Subtract(TimeSpan.FromSeconds(1));

            if (_duration == TimeSpan.Zero)
            {
                var toastxml = @"<toast launch='args' scenario='alarm'>
                                    <visual>
                                        <binding template='ToastGeneric'>
                                            <text>Well done!</text>
                                            <text>Have a rest</text>
                                        </binding>
                                    </visual>
                                    <actions>

                                     <action arguments = 'action=DoSome&amp;'
                                        content = 'Ok' 
                                        activationType = 'background' />

                                    </actions>
                                </toast>";
                var doc = new Windows.Data.Xml.Dom.XmlDocument();
                doc.LoadXml(toastxml);
                var toast = new ToastNotification(doc);
                var notifier = ToastNotificationManager.CreateToastNotifier();
                notifier.Show(toast);
                var data = toast.Data;
            }
        }

        private void SetDuration(bool setWorkingDuration)
        {
            if (setWorkingDuration)
            {
                //Duration = new TimeSpan(0, _settings.WorkingDuration, 00);
                Duration = new TimeSpan(0, 0, 2);
            }
            else
            {
                Duration = new TimeSpan(0, _settings.RestingDuration, 00);
            }
        }

        private void DoSome()
        {
            Debug.WriteLine("jsdlkjflks");
        }
    }
}