using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TikTokTTS
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool LoadLastSessionOnStartup { get; set; } = true;
        public bool SaveOperationsToHistory { get; set; } = true;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
