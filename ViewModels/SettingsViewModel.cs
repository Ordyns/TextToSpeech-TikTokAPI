using System.ComponentModel;

namespace TikTokTTS
{
    public class SettingsViewModel : ViewModelBase
    {
        private UserSettings _settings;
        public UserSettings Settings {
            get => _settings;
            set { _settings = value; OnPropertyChanged(nameof(Settings)); }
        }

        public DelegateCommand SaveCommand { get; } = new DelegateCommand((p) => SettingsSaveSystem.SaveSettingsToFile());
        public DelegateCommand RevertCommand { get; } = new DelegateCommand((p) => SettingsSaveSystem.LoadSettingsFromFile());

        public SettingsViewModel()
        {
            Settings = new UserSettings();
        }

        [System.Serializable]
        public class UserSettings : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            private bool _loadLastSessionOnStartup = true;
            public bool LoadLastSessionOnStartup
            {
                get => _loadLastSessionOnStartup;
                set
                {
                    _loadLastSessionOnStartup = value;
                    OnPropertyChanged(nameof(SaveOperationsToHistory));
                }
            }

            private bool _saveOperationsToHistory = true;
            public bool SaveOperationsToHistory
            {
                get => _saveOperationsToHistory;
                set
                {
                    _saveOperationsToHistory = value;
                    OnPropertyChanged(nameof(SaveOperationsToHistory));
                }
            }
        }
    }
}
