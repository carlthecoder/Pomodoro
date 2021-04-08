using Pomodoro.UWP.Observers;
using Pomodoro.UWP.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Pomodoro.UWP
{
    public sealed partial class MainPage : Page, IPomodorObserver, INotifyPropertyChanged
    {
        private IPomodor pomodor;
        private ISettings settings;
        private TimeSpan duration;

        public TimeSpan Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPage()
        {
            InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(500, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= OnUnloaded;
            pomodor?.UnRegisterObserver(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;

            pomodor = Ioc.Resolve<IPomodor>();
            pomodor.RegisterObserver(this);
            Duration = pomodor.Duration;
            settings = Ioc.Resolve<ISettings>();
            settings.PropertyChanged += OnSettingsChanged;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            pomodor.StartTimer();
            FlipButtonVisibility(showStopButton: true);
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            pomodor.StopTimer();
            FlipButtonVisibility(showStopButton: false);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            pomodor.ResetTimer();
            FlipButtonVisibility(showStopButton: false);
        }

        void IPomodorObserver.NotifyDurationChanged(TimeSpan duration)
        {
            Duration = duration;
        }

        private void FlipButtonVisibility(bool showStopButton)
        {
            if (showStopButton)
            {
                StartButton.Visibility = Visibility.Collapsed;
                StopButton.Visibility = Visibility.Visible;
            }
            else
            {
                StartButton.Visibility = Visibility.Visible;
                StopButton.Visibility = Visibility.Collapsed;
            }
        }

        private void OnSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.IsShowingSettings))
            {
                SettingsControl.Visibility = settings.IsShowingSettings ? Visibility.Visible : Visibility.Collapsed;
                ContentGrid.Visibility = settings.IsShowingSettings ? Visibility.Collapsed : Visibility.Visible;

                if (!settings.IsShowingSettings)
                {
                    pomodor.ResetTimer();
                }
            }
        }

        private void OnSettingsClicked(object sender, RoutedEventArgs e)
        {
            settings.IsShowingSettings = true;
        }

        private async void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (Dispatcher.HasThreadAccess)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
            }
        }
    }
}
