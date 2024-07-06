using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Mirroring
{
    public partial class MainWindow : Window
    {
        private readonly ScreenCapture _screenCapture;
        private readonly Server _server;
        public MainWindow()
        {
            InitializeComponent();
            _screenCapture = new ScreenCapture();
            _server = new Server();
            _server.ServerListener();

        }
        private BitmapImage ConvertByteArrayToBitmapImage(byte[] imageData)
        {
            using (MemoryStream memoryStream = new MemoryStream(imageData))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private void ScreenShot(object sender, RoutedEventArgs e)
        {

            var bitmap = _screenCapture.CaptureScreen();
            var bitmapImage = _screenCapture.ConvertToBitmapImage(bitmap);
            CaptureImage.Source = bitmapImage;
            _screenCapture.BitmapImageToByteArray(bitmapImage);
            

        }



    }
}
