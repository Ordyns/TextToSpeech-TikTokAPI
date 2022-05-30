using Newtonsoft.Json;
using System;
using System.IO;

namespace TikTokTTS
{
    public static class Settings
    {
        public static SettingsViewModel ViewModel = new SettingsViewModel();

        private static readonly string SETTINGS_SAVE_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/TikTokTTS/";
        private static readonly string SETTINGS_SAVE_NAME = "settings.json";
        private static string _saveFullPath => Path.Combine(SETTINGS_SAVE_DIRECTORY, SETTINGS_SAVE_NAME);

        static Settings()
        {
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
                ViewModel = JsonConvert.DeserializeObject<SettingsViewModel>(serialized);
            }
        }

        public static void SaveSettingsToFile()
        {
            if (Directory.Exists(SETTINGS_SAVE_DIRECTORY) == false)
                Directory.CreateDirectory(SETTINGS_SAVE_DIRECTORY);

            File.WriteAllText(_saveFullPath, JsonConvert.SerializeObject(ViewModel));
        }
    }
}
