using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using WebView.ContainUI;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace Microsoft.UI.Xaml
{
    public static class WindowExtensions
    {
        public static nint Handle(this Window window) => WindowNative.GetWindowHandle(window);
    }
}
namespace WebView
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private bool addTabToTabOnStartup = true;

        public IList<TabViewItem> TabItems { get; set; } = new List<TabViewItem>();
        public MainWindow()
        {
            this.InitializeComponent();
            AppWindow.SetIcon("Assets\\AppIcon.ico");
            this.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
            int v = 1;
            if (addTabToTabOnStartup)
            {
                Tabs.Loaded += (sender, e) =>
                {
                    var newTab = new TabViewItem()
                    {
                        Header = "New Document",
                        IconSource = new SymbolIconSource() { Symbol = Symbol.Document },
                    };
                    var frame = new Frame();
                    newTab.Content = frame;
                    frame.Navigate(typeof(TabViewCommandBarContainer));
                    Tabs.TabItems.Add(newTab);
                    Tabs.SelectedItem = newTab;
                };
            }
            if(Application.Current.RequestedTheme==ApplicationTheme.Dark)DwmSetWindowAttribute(this.Handle(),20,ref v,sizeof(int));
        }
        [DllImport("dwmapi.dll")]
        static extern int DwmSetWindowAttribute(nint hWnd, int dwAttribute, ref int pvAttribute, int attrSize);
        private void TabView_AddTabButtonClick(TabView sender, object args)
        {
            var t = new TabViewItem()
            {
                Header = "New Document",
                IconSource = new SymbolIconSource() { Symbol = Symbol.Document },
            };
            Frame frame = new();
            t.Content = frame;
            frame.Navigate(typeof(TabViewCommandBarContainer));
            sender.TabItems.Add(t);
            sender.SelectedItem = t;
        }

        private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

        private void TabView_TabDroppedOutside(TabView sender, TabViewTabDroppedOutsideEventArgs args)
        {
            var window = new MainWindow() { addTabToTabOnStartup = false };
            sender.TabItems.Remove(args.Tab);
            window.Tabs.TabItems.Add(args.Tab);
            window.Tabs.SelectedItem = args.Tab;
            window.Activate();
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
