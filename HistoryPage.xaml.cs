using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TikTokTTS
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();

            HistoryListView.ItemsSource = History.Operations;
        }

        public void UpdateContent()
        {
            HistoryListView.ItemsSource = History.Operations;
            ICollectionView view = CollectionViewSource.GetDefaultView(HistoryListView.ItemsSource);
            view.Refresh();
        }

        public void RemoveOperationFormHistory(object sender, RoutedEventArgs e)
        {
            TextToSpeechOperation textToSpeechOperation = GetOperationFromSender(sender);
            History.RemoveOperation(textToSpeechOperation);
            UpdateContent();
        }

        public void ReuseOperation(object sender, RoutedEventArgs e)
        { 
            TextToSpeechOperation textToSpeechOperation = GetOperationFromSender(sender);
            MainWindow.Instance.MainPage.UseDataFromOperation(textToSpeechOperation);
            MainWindow.Instance.MoveToMainPage();
        }

        public void CopyToClipboard(object sender, RoutedEventArgs e)
        {
            TextToSpeechOperation textToSpeechOperation = GetOperationFromSender(sender);
            Clipboard.SetText(textToSpeechOperation.Text);
        }

        private TextToSpeechOperation GetOperationFromSender(object sender)
        {
            Button button = (Button)sender;
            return (TextToSpeechOperation)button.DataContext;
        }
    }
}
