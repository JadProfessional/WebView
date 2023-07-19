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
using Newtonsoft.Json.Linq;
using Windows.Storage;
using Microsoft.Windows.AppLifecycle;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebView.ContainUI.SettingsViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public static class AppExtensions
    {
        public static void SetAppTheme(this Application app, object value)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string programFolder = Path.Combine(appDataFolder, "Web Browser Example");
            Directory.CreateDirectory(programFolder);
            string filePath = Path.Combine(programFolder, "AppTheme.dat");
            File.WriteAllText(filePath, value.ToString());
        }
    }
    public sealed partial class AppearancePage : Page
    {
        public AppearancePage()
        {
            this.InitializeComponent();
            ComboBox.Loaded += (sender, e) =>
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Web Browser Example"));
                if (File.ReadAllText(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Web Browser Example"), "AppTheme.dat")) == "Light" ||
                    File.ReadAllText(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Web Browser Example"), "AppTheme.dat")) == "Dark")
                {
                    ComboBox.SelectedItem = File.ReadAllText(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Web Browser Example"), "AppTheme.dat"));
                }
                else
                {
                    ComboBox.SelectedItem = "Use system setting";
                }
                ComboBox.SelectionChanged += (sender, e) =>
                {
                    MainWindow window = (MainWindow)(Application.Current as App).m_window;
                    string selectedTheme = string.Empty;
                    string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string programFolder = Path.Combine(appDataFolder, "Web Browser Example");
                    Directory.CreateDirectory(programFolder);
                    string filePath = Path.Combine(programFolder, "AppTheme.dat");
                    File.WriteAllText(filePath, selectedTheme);

                    switch (e.AddedItems[0].ToString())
                    {
                        case "Light":
                            selectedTheme = Enum.Parse(typeof(ApplicationTheme), "Light").ToString();
                            File.WriteAllText(filePath, selectedTheme);
                            break;
                        case "Dark":
                            selectedTheme = Enum.Parse(typeof(ApplicationTheme), "Dark").ToString();
                            File.WriteAllText(filePath, selectedTheme);
                            break;
                        case "Use system setting":
                            selectedTheme = Enum.Parse(typeof(ElementTheme), "Default").ToString();
                            File.WriteAllText(filePath, selectedTheme);
                            break;
                        default:

                            break;
                    }
                    AppInstance.Restart("");
                };
            };
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

    }
}
