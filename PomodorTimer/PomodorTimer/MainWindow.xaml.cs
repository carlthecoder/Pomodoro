using System;
using System.ComponentModel;
using System.Media;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace PomodorTimer
{
    public partial class MainWindow : Window
    {
        private ISettings _settings;

        public static readonly DependencyProperty DurationProperty =
         DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(MainWindow), new PropertyMetadata(new TimeSpan()));

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        private Timer _timer;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
            DataContext = this;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;

            _settings = Ioc.Resolve<ISettings>();
            _settings.PropertyChanged += OnSettingsChanged;

            SetDuration(setWorkingDuration: true);

            _timer = new Timer(1000);
            _timer.Elapsed += Elapsed;
            
        }

        private void OnWorkingClick(object sender, RoutedEventArgs e)
        {
            SetDuration(setWorkingDuration: true);
        }

        private void OnRestingClick(object sender, RoutedEventArgs e)
        {
            SetDuration(setWorkingDuration: false);
        }

        private void OnTimerStartClick(object sender, RoutedEventArgs e)
        {
            StartTimer();
        }

        private void OnTimerStopClick(object sender, RoutedEventArgs e)
        {
            StopTimer();
        }

        private void OnTimerResetClick(object sender, RoutedEventArgs e)
        {
            StopTimer();
            SetDuration(setWorkingDuration: true);
        }

        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.InvokeAsync(() =>
            {
                Duration = Duration.Subtract(TimeSpan.FromSeconds(1));

                if (Duration == TimeSpan.Zero)
                {
                    TriggerAlarm();
                }
            });
        }

        private void TriggerAlarm()
        {
            using (var alarm = new SoundPlayer(@"C:\Windows\WinSxS\amd64_microsoft-windows-shell-sounds_31bf3856ad364e35_10.0.18362.1_none_e96fec2e32c20607\Alarm10.wav"))
            {
                alarm.Play();
            }
        }

        private void StartTimer()
        {
            _timer.Start();
            StartButton.Visibility = Visibility.Collapsed;
            StopButton.Visibility = Visibility.Visible;
            ModePanel.IsEnabled = false;
        }

        private void StopTimer()
        {
            _timer.Stop();
            StartButton.Visibility = Visibility.Visible;
            StopButton.Visibility = Visibility.Collapsed;
            ModePanel.IsEnabled = true;
        }

        private void SetDuration(bool setWorkingDuration)
        {
            if (setWorkingDuration)
            {
                var min = _settings.WorkingDuration;
                Duration = new TimeSpan(0, min, 00);
            }
            else
            {
                var min = _settings.RestingDuration;
                Duration = new TimeSpan(0, min, 00);
            }
        }

        private void OnExitMenuClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void OnSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.WorkingDuration))
            {
                SetDuration(true);
            }
            if (e.PropertyName == nameof(Settings.RestingDuration))
            {
                SetDuration(false);
            }

            if (e.PropertyName == nameof(Settings.IsShowingSettings))
            {                
                settingsView.Visibility = _settings.IsShowingSettings ? Visibility.Visible : Visibility.Collapsed;
                ContentGrid.Visibility = _settings.IsShowingSettings ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void OnSettingsClicked(object sender, RoutedEventArgs e)
        {
            _settings.IsShowingSettings = true;
        }
    }
}
