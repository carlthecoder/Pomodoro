using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PomodorTimer
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsWindow : UserControl
    {
        private ISettings settings;
        public SettingsWindow()
        {
            InitializeComponent();

            settings = Ioc.Resolve<ISettings>();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            WorkDurationBox.Text = settings.WorkingDuration.ToString();
            RestDurationBox.Text = settings.RestingDuration.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            settings.IsShowingSettings = false;
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

            settings.SetDurations(workDuration, restDuration);
            settings.IsShowingSettings = false;
        }
    }
}
