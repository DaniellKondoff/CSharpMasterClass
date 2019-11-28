using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ConfigureAwaitWpf
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Button clicked on thred: {Thread.CurrentThread.ManagedThreadId}");
            await Hello();
        }

        static async Task Hello()
        {
            MessageBox.Show($"Work starting on thred: {Thread.CurrentThread.ManagedThreadId}");
            await DoLongWork();
        }

        static async Task DoLongWork()
        {
            MessageBox.Show($"Work starting on thred: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(1000).ConfigureAwait(false);
            MessageBox.Show($"Work ending on thred: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
