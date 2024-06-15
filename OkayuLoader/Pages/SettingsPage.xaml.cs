using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Storage.Pickers;
using Windows.Storage;

using OkayuLoader.Services;
using System.Threading.Tasks;

namespace OkayuLoader.Pages
{
    public sealed partial class SettingsPage : Page
    {
        private bool allInitializated = false;
        ConfigService configService = new ConfigService();
        Services.UiSettings uiConfig;

        public SettingsPage()
        {
            this.InitializeComponent();

            uiConfig = configService.Load();

            if (uiConfig.customOsuPath == "")
            {
                SettingsCardOsu.Description = "Not initializated!";
            }
            else { SettingsCardOsu.Description = uiConfig.customOsuPath; }

            TextBoxAccount.Text = uiConfig.username;
            PasswordBoxAccount.Password = uiConfig.password;
            CheckBoxDialog.IsChecked = uiConfig.showByeMsgAgain;

            allInitializated = true;
        }

        private async void ButtonSelectOsuHandler(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new();
            folderPicker.FileTypeFilter.Add("*");

            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null) 
            { 
                SettingsCardOsu.Description = folder.Path;
                uiConfig.customOsuPath = folder.Path;
                configService.Save(uiConfig);
            }
        }

        private void TextBoxNicknameHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                uiConfig.username = TextBoxAccount.Text;
                configService.Save(uiConfig);
            }
        }

        private void TextBoxPasswordHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                uiConfig.password = PasswordBoxAccount.Password;
                configService.Save(uiConfig);
            }
        }

        private void CheckBoxDialogHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                uiConfig.showByeMsgAgain = (bool)CheckBoxDialog.IsChecked;
                configService.Save(uiConfig);
            }
        }

        private async void ButtonResetConfigHandler(object sender, RoutedEventArgs e)
        {
            uiConfig.configVersion = 0;
            configService.Save(uiConfig);

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Config reseted!";
            dialog.PrimaryButtonText = "Exit";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = "Okayu Loader must to be restarted!";
            var dialogResultButton = await dialog.ShowAsync();

            await Task.Delay(250);
            System.Environment.Exit(0);
        }
    }
}
