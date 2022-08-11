using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TikTokTTS
{
    public static class HistorySaveSystem
    {
        private static readonly string HistorySavesDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/TikTokTTS/";
        private const string HistorySaveName = "history.json";
        private static readonly string HistorySaveFullpath = Path.Combine(HistorySavesDirectory, HistorySaveName);

        public static void SaveOperations(IEnumerable<TextToSpeechOperation> operations)
        {
            if (SettingsSaveSystem.ViewModel.Settings.SaveOperationsToHistory == false)
                return;

            operations = operations.OrderByDescending(operation => operation.ID).ToList();

            if (Directory.Exists(HistorySavesDirectory) == false)
                Directory.CreateDirectory(HistorySavesDirectory);

            string serealized = JsonConvert.SerializeObject(operations);
            File.WriteAllText(HistorySaveFullpath, serealized);
        }

        public static IEnumerable<TextToSpeechOperation> LoadOperations()
        {
            if (File.Exists(HistorySaveFullpath) == false)
                return new List<TextToSpeechOperation>();

            string serealized = File.ReadAllText(HistorySaveFullpath);
            return JsonConvert.DeserializeObject<List<TextToSpeechOperation>>(serealized);
        }

        public static bool TryLoadLastOperation(out TextToSpeechOperation operation)
        {
            IEnumerable<TextToSpeechOperation> operations = LoadOperations();
            if (operations.Count() > 0)
            {
                operation = operations.First();
                return true;
            }

            operation = null;
            return false;
        }
    }
}
