using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TikTokTTS
{
    public static class History
    {
        private static List<TextToSpeechOperation> _operations = new List<TextToSpeechOperation>();
        public static ReadOnlyCollection<TextToSpeechOperation> Operations => _operations.AsReadOnly();

        private static readonly string HISTORY_SAVE_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/TikTokTTS/";
        private const string HISTORY_SAVE_NAME = "history.json";
        private static readonly string HISTORY_SAVE_FULLPATH = Path.Combine(HISTORY_SAVE_DIRECTORY, HISTORY_SAVE_NAME);

        static History()
        {
            LoadAllOperations();
        }

        public static void SaveAllOperations()
        {
            if (Settings.ViewModel.SaveOperationsToHistory == false)
                return;

            _operations = _operations.OrderByDescending(operation => operation.ID).ToList();

            if (Directory.Exists(HISTORY_SAVE_DIRECTORY) == false)
                Directory.CreateDirectory(HISTORY_SAVE_DIRECTORY);

            string serealized = JsonConvert.SerializeObject(_operations);
            File.WriteAllText(HISTORY_SAVE_FULLPATH, serealized);
        }

        public static void LoadAllOperations()
        {
            if (Settings.ViewModel.SaveOperationsToHistory == false)
                return;

            if (File.Exists(HISTORY_SAVE_FULLPATH) == false)
            {
                _operations = new List<TextToSpeechOperation>();
                return;
            }

            string serealized = File.ReadAllText(HISTORY_SAVE_FULLPATH);
            _operations = JsonConvert.DeserializeObject<List<TextToSpeechOperation>>(serealized);
        }

        public static void AddOperation(TextToSpeechOperation textToSpeechOperation)
        {
            if (Settings.ViewModel.SaveOperationsToHistory == false)
                return;

            if (_operations == null || _operations.Count == 0)
                textToSpeechOperation.ID = 0;
            else
                textToSpeechOperation.ID = _operations.Max(operation => operation.ID) + 1;

            _operations.Add(textToSpeechOperation);
            SaveAllOperations();
        }

        public static void RemoveOperation(TextToSpeechOperation textToSpeechOperation)
        {
            if (Settings.ViewModel.SaveOperationsToHistory == false)
                return;

            _operations.Remove(_operations.Find(operation => operation.ID == textToSpeechOperation.ID));
            SaveAllOperations();
        }
    }
}
