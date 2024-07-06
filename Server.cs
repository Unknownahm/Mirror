using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Media.Imaging;

namespace Mirroring
{
    internal class Server
    {
        private readonly ScreenCapture? _screenCapture;
        public void ServerListener()
        {

            TcpListener? listener = null;
            try
            {
                int port = 1234;
                IPAddress iPAddress = IPAddress.Parse("127.0.0.1");

                listener = new TcpListener(iPAddress, port);
                listener.Start();

                while (true)
                {
                    System.Windows.MessageBox.Show("Waiting for a connection");
                    using TcpClient client = listener.AcceptTcpClient();
                    System.Windows.MessageBox.Show("Connected");

                    NetworkStream stream = client.GetStream();

                    byte[] msg = Encoding.UTF8.GetBytes("Hello dear client");

                    stream.Write(msg, 0, msg.Length);


                    var bitmap = _screenCapture?.CaptureScreen();
                    //var bitmapImage = _screenCapture?.ConvertToBitmapImage(bitmap);
                    //byte[] byteArray = _screenCapture.BitmapImageToByteArray(bitmapImage);
                    //stream.Write(bitmapImage);
                    stream.Write(bitmapImage);
                }



            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"{ex}", "Exception trhown");
            }
            finally
            {
                listener?.Stop();
            }



        }
    }
}
