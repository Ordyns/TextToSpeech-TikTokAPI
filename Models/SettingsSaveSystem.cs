using Newtonsoft.Json;
using System;
using System.IO;

namespace TikTokTTS
{
    public static class SettingsSaveSystem
    {
        public static SettingsViewModel ViewModel;

        private static readonly string SettingsSaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/TikTokTTS/";
        private static readonly string SettingsSaveName = "settings.json";
        private static string _saveFullPath => Path.Combine(SettingsSaveDirectory, SettingsSaveName);

        static SettingsSaveSystem()
        {
            ViewModel = new SettingsViewModel();

            if (File.Exists(_saveFullPath) == false)
                SaveSettingsToFile();
            else
                LoadSettingsFromFile();
        }

        public static void LoadSettingsFromFile()
        {
            if (File.Exists(_saveFullPath))
            {
                string serialized = File.ReadAllText(_saveFullPath);
                ViewModel.Settings = JsonConvert.DeserializeObject<SettingsViewModel.UserSettings>(serialized);
            }
        }

        public static void SaveSettingsToFile()
        {
            if (Directory.Exists(SettingsSaveDirectory) == false)
                Directory.CreateDirectory(SettingsSaveDirectory);

            File.WriteAllText(_saveFullPath, JsonConvert.SerializeObject(ViewModel.Settings));
        }
    }
}
