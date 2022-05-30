using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TikTokTTS
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            VoiceComboBox.ItemsSource = Voices;
            VoiceComboBox.SelectedIndex = 0;

            if (History.Operations.Count == 0)
                return;

            if (Settings.ViewModel.LoadLastSessionOnStartup == false)
                return;

            TextToSpeechOperation operation = History.Operations.Last();
            UseDataFromOperation(operation);
        }

        public string[] Voices { get; set; } = new string[] {
            "en_au_001", "en_au_002", "en_uk_001", "en_uk_003", "en_us_001", "en_us_002", "en_us_006", "en_us_007", "en_us_009", "en_us_010",
            "fr_001", "fr_002", "de_001", "de_002", "es_002",
            "es_mx_002", "br_001", "br_003", "br_004", "br_005",
            "id_001", "jp_001", "jp_003", "jp_005", "jp_006", "kr_002", "kr_003", "kr_004",
            "en_us_ghostface", "en_us_chewbacca", "en_us_c3po", "en_us_stitch", "en_us_stormtrooper", "en_us_rocket"
        };

        private string _outputPath;
        private const int MAX_INPUT_TEXT_LENGTH = 300;

        public void UseDataFromOperation(TextToSpeechOperation operation)
        {
            OutputPathTextBox.Text = operation.Path;
            VoiceComboBox.Text = operation.Voice;
            InputTextBox.Text = operation.Text;
        }

        private void SelectPathButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = "Audio file (*.mp3)|*.mp3|Audio file (*.aac)|*.aac";

            if (_outputPath != null && _outputPath.Length > 0) {
                FileInfo fileInfo = new FileInfo(_outputPath);
                saveFileDialog.FileName = fileInfo.Name;
            }

            if (saveFileDialog.ShowDialog() == true){
                _outputPath = saveFileDialog.FileName;
                OutputPathTextBox.Text = _outputPath;
            }
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _outputPath = OutputPathTextBox.Text;

            if (_outputPath == null || _outputPath.Length == 0)
                return;

            if (InputTextBox.Text.Length > MAX_INPUT_TEXT_LENGTH)
                return;

            LoadingContentDialog dialog = new LoadingContentDialog();
            _ = dialog.ShowAsync();

            TextToSpeechOperation textToSpeechOperation = new TextToSpeechOperation()
            {
                Path = _outputPath,
                Voice = VoiceComboBox.Text,
                Date = DateTime.Now.ToString("HH:mm:ss, dd.MM.yyyy"),
                Text = InputTextBox.Text
            };

            History.AddOperation(textToSpeechOperation);
            byte[] result = await TextToSpeech.CallTikTokAPIAsync(textToSpeechOperation);
            File.WriteAllBytes(_outputPath, result);

            dialog.Hide();
        }

        private void InputTextBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach(string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    if (fileInfo.Extension != "txt")
                        return;

                    if (InputTextBox.Text.Length > 0)
                        InputTextBox.Text += "\n";

                    InputTextBox.Text = File.ReadAllText(file);
                }
            }
        }

        private void LoadTextFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Text file (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true){
                if (InputTextBox.Text.Length > 0)
                    InputTextBox.Text += "\n";

                InputTextBox.Text += File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int inputTextLength = InputTextBox.Text.Length;
            InputTextBoxHeader.Content = $"Text ({inputTextLength } / {MAX_INPUT_TEXT_LENGTH})";

            if (inputTextLength > MAX_INPUT_TEXT_LENGTH)
                InputTextBoxHeader.Foreground = Brushes.Red;
            else
                InputTextBoxHeader.Foreground = Brushes.White;
        }
    }
}
