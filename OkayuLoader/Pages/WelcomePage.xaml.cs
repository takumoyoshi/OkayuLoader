using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Storage.Pickers;
using Windows.Storage;

using OkayuLoader.Services;
using OkayuLoader;

namespace OkayuLoader.Pages
{
    public sealed partial class WelcomePage : Page
    {
        private bool allInitializated = false;
        ConfigService configService = new ConfigService();
        Services.UiSettings uiConfig;

        public WelcomePage()
        {
            this.InitializeComponent();

            uiConfig = configService.Load();

            if (uiConfig.customOsuPath == "")
            {
                SettingsCardOsu.Description = "Not initializated!";
            }
            else { SettingsCardOsu.Description = uiConfig.customOsuPath; }
            if (uiConfig.customPatcherPath == "")
            {
                SettingsCardPatcher.Description = "Not initializated!";
            }
            else { SettingsCardPatcher.Description = uiConfig.customPatcherPath; }

            TextBoxAccount.Text = uiConfig.username;
            PasswordBoxAccount.Password = uiConfig.password;

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

        private async void ButtonSelectPatcherHandler(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new();
            folderPicker.FileTypeFilter.Add("*");

            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                SettingsCardPatcher.Description = folder.Path;
                uiConfig.customPatcherPath = folder.Path;
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

        private async void ButtonFinishHandler(object sender, RoutedEventArgs e)
        {
            if (uiConfig.username == "" || uiConfig.password == "" || uiConfig.customOsuPath == "" || uiConfig.customPatcherPath == "")
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.XamlRoot;
                dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                dialog.Title = "Some values are empty!";
                dialog.PrimaryButtonText = "OK";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "Please, input some data in the text boxes or select folders.";
                var dialogResultButton = await dialog.ShowAsync();

                return;
            }
            else
            {
                this.Frame.Navigate(typeof(HomePage));
                MainWindow.CurrentInstance.NaviagateTo(0);
                uiConfig.welcome = false;
                configService.Save(uiConfig);
            }
        }
    }
}
