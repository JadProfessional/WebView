using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebView.Helper;
using WinRT.Interop;
namespace WebView.Helper
{
    public static class WindowHelper
    {
        public static Window CreateWindow()
        {
            var window = new Window();
            TrackWindow(window);
            return window;
        }
        public static void TrackWindow(Window item)
        {            
            item.Closed += (sender, args) => ActiveWindows.Remove(item);
            ActiveWindows.Add(item);
        }

        public static Window GetWindowForElement(UIElement element)
        {
            if (element.XamlRoot != null)
            {
                foreach (var window in ActiveWindows)
                {
                    if (element.XamlRoot == window.Content.XamlRoot)
                    {
                        return window;
                    }
                }
            }
            return (Application.Current as App).m_window;
        }
        public static IList<Window> ActiveWindows { get; } = new List<Window>();
    }
}
