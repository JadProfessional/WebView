using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using WebView.Assets;
using WebView.Helper;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebView.ContainUI
{
    public sealed partial class TabViewCommandBarContainer : Page
    {
        string Uri
        {
            get
            {
                if (uriBox.Text.StartsWith("https://"))
                {
                    return webViewSample.Source.ToString();
                }
                else if (uriBox.Text.StartsWith("http://"))
                {
                    return $"{webViewSample.Source?.Host}/{webViewSample.Source?.Fragment}";
                }
                else 
                {
                    return string.Empty;
                }
            }
        }
        private string DefaultUri => webViewSample.Source.ToString();
        public Request Request { get; private set; }
        public string NavigateUri
        {
            get
            {
                return uriBox.Text;
            }
            set
            {

            }
        }
        public void SetRequest(Request value)
        {
            Request = value;
            if (value == Request.OpenSettings)
            {
                fr.Visibility = Visibility.Visible;
                fr.Navigate(typeof(SettingsPage), null, new SuppressNavigationTransitionInfo());
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string uri = e.Parameter?.ToString();
            base.OnNavigatedTo(e);
        }
        public Window CurrentWindow => WindowHelper.GetWindowForElement(this);
        public TabViewCommandBarContainer()
        {
            this.InitializeComponent();
            var window = CurrentWindow;
            contain.Height = window.Bounds.Height;
            contain.Width = window.Bounds.Width;
            window.SizeChanged += (sender, args) =>
            {
                contain.Height = args.Size.Height;
                contain.Width = args.Size.Width;
            };
            if (string.IsNullOrEmpty(uriBox.Text))
            {
                webViewSample.Visibility = Visibility.Collapsed;
                fr.Visibility = Visibility.Visible;
                fr.Navigate(typeof(NewTabPage), this, new SuppressNavigationTransitionInfo());    
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (webViewSample.CanGoBack) webViewSample.GoBack(); else (sender as Control).IsEnabled = false;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (webViewSample.CanGoForward) webViewSample.GoForward(); else (sender as Control).IsEnabled = false;

        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            var frameContent = fr.Content as NewTabPage;
            frameContent?.Frame.Navigate(typeof(NewTabPage), this, new SuppressNavigationTransitionInfo());
            try
            {
                webViewSample.Reload();
            }
            catch (Exception)
            {

            }
        }

        private void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FavoriteBar_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as CustomDataObject;
            webViewSample.Source = new(clickedItem.Uri.ToString());
        }

        private void UriBox_GotFocus(object sender, RoutedEventArgs e) => (sender as TextBox).SelectAll();

        private void UriBox_Loaded(object sender, RoutedEventArgs e) => (sender as UIElement).Focus(FocusState.Programmatic);

        private void UriBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            try
            {
                if (e.Key == VirtualKey.Enter)
                {
                    if (!(sender as TextBox).Text.StartsWith("http://") | !(sender as TextBox).Text.StartsWith("https://"))
                    {
                        webViewSample.Source = new($"http://{(sender as TextBox).Text}");
                    }
                    webViewSample.Source = new((sender as TextBox).Text);
                }
            }
            catch (Exception)
            {
                webViewSample.Source = new($"http://{(sender as TextBox).Text}");
            }
        }

        private void KeyboardAccelerator_Invoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            OverflowMenu.Focus(FocusState.Programmatic);
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            WindowHelper.GetWindowForElement(this).Close();
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            fr.Navigate(typeof(SettingsPage), this, new SuppressNavigationTransitionInfo());
        }
    }
    public enum Request
    {
        OpenSettings,
        NavigateToUri
    }
}
