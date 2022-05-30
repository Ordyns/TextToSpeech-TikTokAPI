using System.Windows;
using System.Windows.Controls;

namespace TikTokTTS
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            DataContext = Settings.ViewModel;
        }

        private void RevertSettings(object sender, RoutedEventArgs e)
        {
            Settings.LoadSettingsFromFile();
            LoadLastSessionOnStartupCheckBox.IsChecked = Settings.ViewModel.LoadLastSessionOnStartup;
            SaveOperationsToHistoryCheckBox.IsChecked = Settings.ViewModel.SaveOperationsToHistory;
        }

        private void ApplySettings(object sender, RoutedEventArgs e) { 
            Settings.ViewModel.LoadLastSessionOnStartup = (bool)LoadLastSessionOnStartupCheckBox.IsChecked;
            Settings.ViewModel.SaveOperationsToHistory = (bool)SaveOperationsToHistoryCheckBox.IsChecked;
            Settings.SaveSettingsToFile();
        }
    }
}
