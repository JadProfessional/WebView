using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using WebView.Assets;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebView.ContainUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewTabPage : Page
    {
        ObservableCollection<CustomDataObject> Favorites { get; } = new();

        public NewTabPage()
        {
            this.InitializeComponent();
        }
        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            favoriteBar.ItemClick += (sender, args) =>
            {
                var clickedItem = args.ClickedItem as CustomDataObject;
                var webViewSample = (e.Parameter as TabViewCommandBarContainer).webViewSample;
                webViewSample.Source = new(clickedItem.Uri.ToString());
            };
            base.OnNavigatedTo(e);
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var panel = new StackPanel();
            var required = new TextBlock()
            {
                Text = "All fields are mandatory/required"
            };
            var textBox1 = new TextBox()
            {
                Header = "Name"
            };
            var textBox2 = new TextBox()
            {
                Header = "URI"
            };
            var textBox3 = new TextBlock()
            {
                Visibility = Visibility.Collapsed,
                Text = "Please fill out the required fields.",
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["SystemErrorTextColor"])
            };
            panel.Children.Add(textBox1);
            panel.Children.Add(textBox2);
            panel.Children.Add(textBox3);
            var dialog = new ContentDialog()
            {
                Title = "Add favorite",
                Content = panel,
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = this.XamlRoot
            };
            dialog.Opened += (sender, e) =>
            {
                textBox1.Focus(FocusState.Programmatic);
            };
            dialog.PrimaryButtonClick += (sender, e) =>
            {
                var name = textBox1.Text;
                try
                {
                    var uri = new Uri(textBox2.Text);

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(uri.ToString()))
                    {
                        Favorites.Add(new()
                        {
                            Name = name,
                            Uri = uri
                        });
                    }
                    else
                    {
                        e.Cancel = true;
                        textBox3.Text = "Please enter a valid name.";
                        textBox3.Visibility = Visibility.Visible;
                        if (string.IsNullOrEmpty(name))
                        {
                            textBox1.Focus(FocusState.Programmatic);
                        }
                        else if (string.IsNullOrEmpty(uri.ToString()))
                        {
                            textBox2.Focus(FocusState.Programmatic);
                        }
                        else if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(uri.ToString()))
                        {
                            textBox1.Focus(FocusState.Programmatic);
                        }
                    }
                }
                catch (Exception)
                {
                    e.Cancel = true;
                    textBox3.Visibility = Visibility.Visible;
                    textBox3.Text = "Please enter a valid URL.";

                    textBox2.Focus(FocusState.Programmatic);
                    if (!string.IsNullOrEmpty(name) || string.IsNullOrEmpty(textBox2.Text))
                    {
                        textBox2.Focus(FocusState.Programmatic);
                    }
                }
            };
            var result = await dialog.ShowAsync();
        }
    }
}
