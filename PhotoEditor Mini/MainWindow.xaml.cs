using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace PhotoEditor_Mini
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files|*.png; *.jpg";
            bool? state = ofd.ShowDialog();

            if (state == true)
            {
                Uri path = new Uri(ofd.FileName);
                cnvsImage.Source = new BitmapImage(path);
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e) => cnvsImage.Source = null;
    }
}