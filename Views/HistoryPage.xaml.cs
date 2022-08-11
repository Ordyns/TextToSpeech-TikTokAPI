using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace TikTokTTS
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        private HistoryViewModel _viewModel;

        public HistoryPage()
        {
            InitializeComponent();

            _viewModel = History.ViewModel;
            DataContext = _viewModel;
        }

        private void HistoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedOperations.Clear();
            List<TextToSpeechOperation> selectedItems = HistoryListView.SelectedItems.Cast<TextToSpeechOperation>().ToList();
            selectedItems.ForEach(item => _viewModel.SelectedOperations.Add(item));
        }
    }
}
