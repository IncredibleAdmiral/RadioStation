using RadioStation.Model.Validation;
using RadioStation.Model.Validation.PartnerReflection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RadioStation.Model.Network
{
    class Listner
    {
        public static void Listen()
        {
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["PortTCPListner"]));

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);



                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        ReflectionOfConditionPartnerStation.SetCondition(builder.ToString());
                    }
                    while (handler.Available > 0);

                    if (ConfigurationManager.AppSettings["debug"] == "true")
                    {
                        MessageBox.Show(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                    }



                    if( builder[0] != 'A')
                    {
                        // отправляем ответ
                        string message = "A" + ConditionStringMaker.GetConditionString();
                        data = Encoding.Unicode.GetBytes(message);
                        handler.Send(data);
                    }
                    

                    
                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
