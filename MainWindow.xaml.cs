using System;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using System.Runtime.InteropServices;

using OkayuLoader.Pages;
using OkayuLoader.Services;

namespace OkayuLoader
{

    public sealed partial class MainWindow : Window
    {
        ConfigService configService = new ConfigService();
        Services.UiSettings uiConfig;

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        public const int DESKTOPVERTRES = 117;
        public const int DESKTOPHORZRES = 118;

        public void NaviagateTo(int index)
        {
            NavigationView.SelectedItem = NavigationView.MenuItems[index];
        }

        public static MainWindow CurrentInstance { get; private set; }
        public MainWindow()
        {
            this.InitializeComponent();
            CurrentInstance = this;

            TitleBarTextBlock.Text = AppInfo.Current.DisplayInfo.DisplayName;
            ExtendsContentIntoTitleBar = true;

            IntPtr screenDC = GetDC(IntPtr.Zero);
            int width = GetDeviceCaps(screenDC, DESKTOPHORZRES);
            int height = GetDeviceCaps(screenDC, DESKTOPVERTRES);

            AppWindow.Resize(new Windows.Graphics.SizeInt32 { Width = 1000, Height = 575 });
            AppWindow.Move(new Windows.Graphics.PointInt32 { X = (width / 2 - 500), Y = (height / 2 - 300) });

            uiConfig = configService.Load();
        }

        private void NavigationViewInit(object sender, RoutedEventArgs args)
        {
            Type pageType;
            if (uiConfig.welcome == true) 
            { 
                pageType = typeof(WelcomePage);
                NavigationView.Header = "Welcome! Let's setup some settings";
            }
            else
            {
                pageType = typeof(HomePage);
                NavigationView.SelectedItem = NavigationView.MenuItems[0];
            }
            
            _ = contentFrame.Navigate(pageType);
        }

        private void NavigationViewBehavior(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;
            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                navOptions.IsNavigationStackEnabled = false;
            }
            Type pageType = typeof(HomePage);

            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem.Name == NvTabHome.Name)
            {
                pageType = typeof(HomePage);
                NavigationView.Header = "Okayu Loader";
            }
            else if (selectedItem.Name == NvTabSettings.Name)
            {
                pageType = typeof(SettingsPage);
                NavigationView.Header = "Settings";
            }

            _ = contentFrame.Navigate(pageType);
        }
    }
}