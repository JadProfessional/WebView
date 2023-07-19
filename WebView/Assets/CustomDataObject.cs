using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebView.Assets
{
    public class CustomDataObject
    {
        public string Name
        {
            get; set;
        }
        public ImageSource Icon => new BitmapImage(new($"https://www.google.com/s2/favicons?domain={Uri}"));

        public Uri Uri { get; set; }
    }
}
