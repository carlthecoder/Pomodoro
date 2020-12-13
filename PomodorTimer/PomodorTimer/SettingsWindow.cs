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
    public partial class SettingsWindow : Window
    {
        private Lazy<Settings> _lazySettings = new Lazy<Settings>(Ioc.Resolve<Settings>());
        private Settings Settings => _lazySettings.Value;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

            Settings.SetDurations(workDuration, restDuration);
            this.Close();
        }
    }
}
