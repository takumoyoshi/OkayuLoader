using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

using OkayuLoader.Services;
using OkayuLoader.Tools;

namespace OkayuLoader.Pages
{
    public sealed partial class HomePage : Page
    {
        private bool allInitializated = false;
        FileEdit fileEditTool = new FileEdit();
        ConfigService configService = new ConfigService();
        Services.UiSettings uiConfig;

        public HomePage()
        {
            this.InitializeComponent();

            uiConfig = configService.Load();
            ToggleSwitchAutoLogin.IsOn = uiConfig.useAutoLogin;
            ToggleSwitchPatcher.IsOn = uiConfig.isPatcherEnabled;

            allInitializated = true;
        }

        private void ToggleSwitchAutoLoginHandler(object sender, RoutedEventArgs e)
        {
                if (ToggleSwitchAutoLogin.IsOn == true) uiConfig.useAutoLogin = true;
                if (ToggleSwitchAutoLogin.IsOn == false) uiConfig.useAutoLogin = false;
                configService.Save(uiConfig);
        }

        private void ToggleSwitchPatcherHandler(object sender, RoutedEventArgs e)
        {
            if (ToggleSwitchPatcher.IsOn == true) uiConfig.isPatcherEnabled = true;
            if (ToggleSwitchPatcher.IsOn == false) uiConfig.isPatcherEnabled = false;
            configService.Save(uiConfig);
        }

        private async void ButtonPlayHandler(object sender, RoutedEventArgs e)
        {
            ButtonPlay.Content = "Starting osu!...";
            ButtonPlay.IsEnabled = false;
            string osuFolderPath = uiConfig.customOsuPath;
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string username = Environment.UserName;

            if (uiConfig.useAutoLogin) { fileEditTool.ChangeAccountForOsu((osuFolderPath + "\\osu!." + username + ".cfg"), uiConfig.username, uiConfig.password); }

            await Task.Delay(1000);

            var osuProcessHandler = new Process();
            osuProcessHandler.StartInfo.FileName = osuFolderPath + "\\osu!.exe";
            osuProcessHandler.StartInfo.Arguments = "-devserver osuokayu.moe";
            osuProcessHandler.Start();

            if (ToggleSwitchPatcher.IsOn == true)
            {
                await Task.Delay(2000);
                var patcherProcessHandler = new Process();
                patcherProcessHandler.StartInfo.FileName = userFolderPath + "\\.OkayuLoader\\Osu!Patcher\\osu!.patcher.exe";
                patcherProcessHandler.Start();
            }

            if (uiConfig.showByeMsgAgain)
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.XamlRoot;
                dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                dialog.Title = "Okayu Loader finished its work!";
                dialog.PrimaryButtonText = "OK";
                dialog.SecondaryButtonText = "Don't show this window again";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "You can close this window by clicking \"Ok\" button. Osu! will be running without loader. Thank you for using my loader!";
                var dialogResultButton = await dialog.ShowAsync();

                if (dialogResultButton == ContentDialogResult.Secondary)
                {
                    uiConfig.showByeMsgAgain = false;
                    configService.Save(uiConfig);

                    await Task.Delay(250);
                    System.Environment.Exit(0);
                }
            }
            await Task.Delay(250);

            if (!uiConfig.showByeMsgAgain)
            { 
                System.Environment.Exit(0);
            }

            ButtonPlay.Content = "Run osu!";
            ButtonPlay.IsEnabled = true;
        }
    }
}
