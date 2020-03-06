using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace RadioStation.Model.Network
{
    class Sender
    {
        public static void Send(string message)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["IP"]), int.Parse(ConfigurationManager.AppSettings["PortTCPSender"]));

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);


                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);

                if (ConfigurationManager.AppSettings["debug"] == "true")
                {
                    MessageBox.Show("ответ сервера: " + builder.ToString());
                }

               

                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
    }
}
