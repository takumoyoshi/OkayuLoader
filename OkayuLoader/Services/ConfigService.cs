using System;
using System.IO;
using System.Text.Json;

namespace OkayuLoader.Services
{

    public class UiSettings
    {
        public string customOsuPath { get; set; }
        public string customPatcherPath { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isPatcherEnabled { get; set; }
        public bool showByeMsgAgain { get; set; }
        public bool useAutoLogin { get; set; }
        public bool welcome { get; set; }
        public int configVersion { get; set; }
    }

    public class ConfigService
    {
        const int reqVerisonConfig = 10;

        public void CreateConfigFile()
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathConfigFolder = System.IO.Path.Combine(userFolderPath, ".OkayuLoader");
            string pathConfigFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\UiSettings.cfg");

            if (Directory.Exists(pathConfigFolder))
            {
                Directory.Delete(pathConfigFolder, true);
            }

            var uiSettings = new UiSettings
            {
                customOsuPath = "",
                customPatcherPath = "",
                username = "",
                password = "",
                isPatcherEnabled = false,
                showByeMsgAgain = true,
                useAutoLogin = false,
                welcome = true,
                configVersion = reqVerisonConfig
            };
            string jsonConfig = JsonSerializer.Serialize<UiSettings>(uiSettings);

            Directory.CreateDirectory(pathConfigFolder);
            File.WriteAllText(pathConfigFile, jsonConfig);
        }

        public UiSettings Load()
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathConfigFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\UiSettings.cfg");

            bool fileExists = File.Exists(pathConfigFile);
            if (fileExists == false)
            {
                CreateConfigFile();
            }

            var config = JsonSerializer.Deserialize<UiSettings>(File.ReadAllText(pathConfigFile));
            if (config.configVersion < reqVerisonConfig)
            {
                CreateConfigFile();
                return JsonSerializer.Deserialize<UiSettings>(File.ReadAllText(pathConfigFile));
            }
            return config;
        }

        public void Save(UiSettings currentSettings)
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathConfigFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\UiSettings.cfg");

            File.Delete(pathConfigFile);
            string jsonConfig = JsonSerializer.Serialize(currentSettings);
            File.WriteAllText(pathConfigFile, jsonConfig);
        }
    }
}