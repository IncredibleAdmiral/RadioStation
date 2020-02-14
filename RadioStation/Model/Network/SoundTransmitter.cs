using NAudio.Wave;
using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RadioStation.Model.Network
{
    class SoundTransmitter
    {
        static private bool connected;
        //сокет отправитель
        static Socket client;
        //поток для нашей речи
        static WaveIn input;
        //поток для речи собеседника
        static WaveOut output;
        //буфферный поток для передачи через сеть
        static BufferedWaveProvider bufferStream;
        //поток для прослушивания входящих сообщений
        static Thread in_thread;
        //сокет для приема (протокол UDP)
        static Socket listeningSocket;

        static IPAddress remoteAdress;

        static public void InitComponents(IPAddress ip)
        {
            if (remoteAdress != null)
                Dispose();

            remoteAdress = ip;
            input = new WaveIn();
            //определяем его формат - частота дискретизации 8000 Гц, ширина сэмпла - 16 бит, 1 канал - моно
            input.WaveFormat = new WaveFormat(8000, 16, 1);
            //добавляем код обработки нашего голоса, поступающего на микрофон
            input.DataAvailable += Voice_Input;
            //создаем поток для прослушивания входящего звука
            output = new WaveOut();
            //создаем поток для буферного потока и определяем у него такой же формат как и потока с микрофона
            bufferStream = new BufferedWaveProvider(new WaveFormat(8000, 16, 1));
            //привязываем поток входящего звука к буферному потоку
            output.Init(bufferStream);
            //сокет для отправки звука
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            connected = true;
            listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //создаем поток для прослушивания
            in_thread = new Thread(new ThreadStart(Listening));
            //запускаем его
            in_thread.Start();
        }

        static public void Voice_Input(object sender, WaveInEventArgs e)
        {
            try
            {
                //Подключаемся к удаленному адресу
                IPEndPoint remote_point = new IPEndPoint(remoteAdress, int.Parse(ConfigurationManager.AppSettings["PortUDP"]));
                //посылаем байты, полученные с микрофона на удаленный адрес
                client.SendTo(e.Buffer, remote_point);

            }
            catch (Exception ex)
            {

            }
        }

        static public void Listening()
        {
            //Прослушиваем по адресу
            //IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            IPEndPoint localIP = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["PortUDP"]));
            listeningSocket.Bind(localIP);
            //начинаем воспроизводить входящий звук
            output.Play();
            //адрес, с которого пришли данные
            EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);
            //бесконечный цикл
            while (connected == true)
            {
                try
                {
                    //промежуточный буфер
                    byte[] data = new byte[65535];
                    //получено данных
                    int received = listeningSocket.ReceiveFrom(data, ref remoteIp);
                    //добавляем данные в буфер, откуда output будет воспроизводить звук
                    bufferStream.AddSamples(data, 0, received);
                }
                catch (SocketException ex)
                {
                   
                }
            }
        }

        public static void Record()
        {
            input.StartRecording();            
        }
        public static void StopRecord()
        {
            try
            {
                input.StopRecording();
            }
            catch
            {
            }
        }
        static public void Dispose()
        {
            connected = false;
            if (listeningSocket != null)
            {
                listeningSocket.Close();

                listeningSocket.Dispose();
            }
            if (client != null)
            {
                client.Close();
                client.Dispose();
            }
            if (output != null)
            {
                output.Stop();
                output.Dispose();
                output = null;
            }
            if (input != null)
            {
                input.Dispose();
                input = null;
            }
            bufferStream = null;
        }
    }
}
