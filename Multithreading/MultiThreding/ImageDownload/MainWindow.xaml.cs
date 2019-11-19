using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageDownload
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                var image = new Image();
                int count = i;
                var t = new Thread(() =>
                {
                    LoadImage("https://http.cat/40" + count, image);
                });
                t.Start();
            }
        }

        public void LoadImage(string url, Image image)
        {
            WebClient wc = new WebClient();
            byte[] byteImg = wc.DownloadData(url);

            Dispatcher.Invoke(() =>
            {
                var bitMapImage = ToImage(byteImg);
                image.Source = bitMapImage;
                Container.Children.Add(image);
            });
        }

        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
