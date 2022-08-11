using System;
using System.IO;

namespace TikTokTTS
{
    public class MainPageViewModel : ViewModelBase
    {
        public string[] Voices { get; } = new string[] {
            "en_au_001", "en_au_002", "en_uk_001", "en_uk_003", "en_us_001", "en_us_002", "en_us_006", "en_us_007", "en_us_009", "en_us_010",
            "fr_001", "fr_002", "de_001", "de_002", "es_002",
            "es_mx_002", "br_001", "br_003", "br_004", "br_005",
            "id_001", "jp_001", "jp_003", "jp_005", "jp_006", "kr_002", "kr_003", "kr_004",
            "en_us_ghostface", "en_us_chewbacca", "en_us_c3po", "en_us_stitch", "en_us_stormtrooper", "en_us_rocket"
        };

        private string _outputPath;
        public string OutputPath
        {
            get => _outputPath;
            set { _outputPath = value; OnPropertyChanged(nameof(OutputPath)); }
        }

        public const int MaxInputTextLength = 300;
        private string _text;
        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(nameof(Text)); }
        }

        private string _voice;
        public string Voice
        {
            get => _voice;
            set { _voice = value; OnPropertyChanged(nameof(Voice)); }
        }

        public event Action StartCommandFinished;
        public DelegateCommand StartCommand { get; }

        public MainPageViewModel()
        {
            StartCommand = new DelegateCommand(OnStartCommand, (p) => Text.Length <= MaxInputTextLength);

            if (SettingsSaveSystem.ViewModel.Settings.LoadLastSessionOnStartup == false)
                return;

            if (HistorySaveSystem.TryLoadLastOperation(out TextToSpeechOperation operation))
                UseDataFromOperation(operation);
        }

        private async void OnStartCommand(object parameter)
        {
            if (string.IsNullOrEmpty(OutputPath) || Text.Length > MaxInputTextLength)
                return;

            if (Directory.Exists(new FileInfo(OutputPath).Directory.FullName))
            { 
                TextToSpeechOperation operation = new TextToSpeechOperation() { Text = _text, Path = _outputPath, Voice = _voice };
                History.ViewModel.AddOperation(operation);
                await TextToSpeech.SaveTextAsSpeech(operation);
            }

            StartCommandFinished?.Invoke();
        }

        public void UseDataFromOperation(TextToSpeechOperation operation)
        {
            OutputPath = operation.Path;
            Voice = operation.Voice;
            Text = operation.Text;
        }
    }
}
