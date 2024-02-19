using System.IO;
using System.Windows;
using System.Windows.Media;
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

            if (ofd.ShowDialog() == true)
            {
                Uri path = new Uri(ofd.FileName);
                cnvsImage.Source = new BitmapImage(path);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => cnvsImage.Source = null;

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cnvsImage.Source != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PNG Image|*.png | JPG Image|*.jpg";

                if (sfd.ShowDialog() == true)
                {
                    string filePath = sfd.FileName;
                    BitmapEncoder? encoder = null;

                    switch (Path.GetExtension(filePath).ToLower())
                    {
                        case ".png":
                            encoder = new PngBitmapEncoder();
                            break;
                        case ".jpg":
                        case ".jpeg":
                            encoder = new JpegBitmapEncoder();
                            break;
                        default:
                            MessageBox.Show("The wrong file type is selected", "Wrong File Type", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                    }

                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)cnvsImage.Source));

                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        encoder.Save(stream);
                    }

                    MessageBox.Show("The image was saved successfully.");
                }
            }
            else
            {
                MessageBox.Show("There is no image to save.", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRotate_Click(object sender, RoutedEventArgs e)
        {
            if (cnvsImage.Source != null)
            {
                BitmapSource source = (BitmapSource)cnvsImage.Source;

                RotateTransform rotateTransform = new RotateTransform(90);

                TransformedBitmap rotatedBitmap = new TransformedBitmap(source, rotateTransform);

                cnvsImage.Source = rotatedBitmap;
            }
            else
            {
                MessageBox.Show("There is no image to rotate.", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}