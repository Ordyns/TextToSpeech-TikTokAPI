using Microsoft.Win32;
using System;
using System.IO;
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
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            DataContext = _viewModel;
            VoiceComboBox.SelectedIndex = 0;
        }

        private void SelectPathButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = ShowFileDialog(new SaveFileDialog(), "Audio file (*.mp3)|*.mp3|Audio file (*.aac)|*.aac", true);

            if (string.IsNullOrEmpty(fileName) == false)
                _viewModel.OutputPath = fileName;
        }

        private void LoadTextFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = ShowFileDialog(new OpenFileDialog(), "Text file (*.txt)|*.txt", false);

            if (string.IsNullOrEmpty(fileName) == false){
                _viewModel.Text += _viewModel.Text.Length > 0 ? "\n" : string.Empty;
                _viewModel.Text += File.ReadAllText(fileName);
            }
        }

        private string ShowFileDialog(FileDialog fileDialog, string filter, bool useOutputFilename)
        {
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fileDialog.Filter = filter;

            if (string.IsNullOrEmpty(_viewModel.OutputPath) == false)
            {
                fileDialog.InitialDirectory = Path.GetDirectoryName(_viewModel.OutputPath);
                fileDialog.DefaultExt = new FileInfo(_viewModel.OutputPath).Extension;
                if(useOutputFilename)
                    fileDialog.FileName = new FileInfo(_viewModel.OutputPath).Name;
            }

            return fileDialog.ShowDialog() == true ? fileDialog.FileName : string.Empty;
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int inputTextLength = InputTextBox.Text.Length;
            InputTextBoxHeader.Content = $"Text ({inputTextLength } / {MainPageViewModel.MaxInputTextLength})";
            InputTextBoxHeader.Foreground = inputTextLength > MainPageViewModel.MaxInputTextLength ? Brushes.Red : Brushes.White;

            StartButton.IsEnabled = inputTextLength <= MainPageViewModel.MaxInputTextLength;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingContentDialog dialog = new LoadingContentDialog();
            dialog.ShowAsync();

            _viewModel.StartCommandFinished += dialog.Hide;
            _viewModel.StartCommand.Execute(e);
        }

        public void ReuseOperation(TextToSpeechOperation operation) => _viewModel.UseDataFromOperation(operation);
    }
}
