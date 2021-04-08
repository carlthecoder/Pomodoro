using Pomodoro.UWP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Pomodoro.UWP
{
    public sealed partial class SettingsControl : UserControl
    {
        private ISettings _settings = Ioc.Resolve<ISettings>();

        private int WorkDuration { get; set; } = 25;
        private int RestDuration { get; set; } = 5;

        public SettingsControl()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            if (_settings == null)
            {
                return;
            }
            WorkDuration = _settings.WorkingDuration;
            RestDuration = _settings.RestingDuration;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _settings.IsShowingSettings = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check Input
            var workDurationInput = WorkDurationBox.Text;
            var restDurationInput = RestDurationBox.Text;

            if (!int.TryParse(workDurationInput, out int workDuration))
            {
                return;
            }

            if (!int.TryParse(restDurationInput, out int restDuration))
            {
                return;
            }

            _settings.SetDurations(workDuration, restDuration);
            _settings.IsShowingSettings = false;
        }
    }
}
