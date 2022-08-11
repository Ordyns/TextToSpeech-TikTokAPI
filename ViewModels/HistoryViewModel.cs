using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Windows;

namespace TikTokTTS
{
    public class HistoryViewModel : ViewModelBase
    {
        public ObservableCollection<TextToSpeechOperation> _operations;
        public ReadOnlyObservableCollection<TextToSpeechOperation> Operations { get; }

        public ObservableCollection<TextToSpeechOperation> SelectedOperations { get; set; }

        public ICommand ReuseOperationCommand { get; }
        public ICommand CopyOperationsCommand { get; }
        public ICommand DeleteOperationsCommand { get; }

        public bool CanReuse => SelectedOperations != null && SelectedOperations.Count == 1;

        public HistoryViewModel(IEnumerable<TextToSpeechOperation> operations)
        {
            ReuseOperationCommand = new DelegateCommand(OnReuseOperationsCommand);
            CopyOperationsCommand = new DelegateCommand(OnCopyOperationsCommand);
            DeleteOperationsCommand = new DelegateCommand(OnDeleteOperationsCommand);

            _operations = new ObservableCollection<TextToSpeechOperation>(operations);
            _operations.CollectionChanged += (s, e) => HistorySaveSystem.SaveOperations(_operations);
            Operations = new ReadOnlyObservableCollection<TextToSpeechOperation>(_operations);

            SelectedOperations = new ObservableCollection<TextToSpeechOperation>();
            SelectedOperations.CollectionChanged += OnSelectedOperationsCollectionChanged;
        }

        public HistoryViewModel() : this(new List<TextToSpeechOperation>()) { }

        private void OnReuseOperationsCommand(object commandParameter)
        {
            if (SelectedOperations.Count != 1)
                return;

            MainWindow.Instance.MainPage.ReuseOperation(SelectedOperations.First());
            MainWindow.Instance.MoveToMainPage();
        }

        private void OnDeleteOperationsCommand(object commandParameter)
        {
            for(int i = SelectedOperations.Count; i > 0; i--)
                _operations.Remove(SelectedOperations[0]);
        }

        private void OnCopyOperationsCommand(object commandParameter)
        {
            List<TextToSpeechOperation> operations = SelectedOperations.ToList();
            string operationsText = string.Empty;

            if (operations.Count > 1)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < operations.Count; i++)
                {
                    stringBuilder.Append($"[{operations[i].Date}]");
                    stringBuilder.AppendLine("\n" + operations[i].Text + "\n");
                }

                operationsText = stringBuilder.ToString();
            }
            else if (operations.Count == 1)
            {
                operationsText = operations[0].Text;
            }

            Clipboard.SetText(operationsText);
        }

        public void AddOperation(TextToSpeechOperation textToSpeechOperation)
        {
            textToSpeechOperation.ID = _operations.Count == 0 ? 0 : _operations.Max(operation => operation.ID) + 1;
            _operations.Insert(0, textToSpeechOperation);
        }

        private void OnSelectedOperationsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(CanReuse));
    }
}
