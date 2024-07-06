using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Mirroring
{
    internal class ScreenCapture
    {
        public Bitmap CaptureScreen()
        {

            var screenSize = Screen.PrimaryScreen.Bounds.Size;
            var bitmap = new Bitmap(1920, 1080);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, screenSize);
            }

            return bitmap;
        }
        public BitmapImage ConvertToBitmapImage(Bitmap bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        public byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public byte[] SendImage(MemoryStream memoryStream) 
        {

        }
    }
}
