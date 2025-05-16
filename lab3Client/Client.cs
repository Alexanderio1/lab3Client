using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace lab3Client
{
    public class Client
    {
        private const int BufferSize = 2_000_000;
        private const int DefaultPort = 8888;

        private readonly TcpClient _tcp;
        private readonly IPAddress _addr;
        private NetworkStream _stream;

        public bool Connected => _tcp.Connected;

        public Client(string ip)
        {
            _addr = IPAddress.Parse(ip);
            _tcp = new TcpClient();
        }

        public void Connect()
        {
            _tcp.Connect(_addr, DefaultPort);
            _stream = _tcp.GetStream();
            Log($"Соединено с {_addr}:{DefaultPort}");
        }

        public void Close()
        {
            Send("?Disconnect");
            _stream.Close();
            _tcp.Close();
            Log("Отключено от сервера");
        }

        public void Send(string msg)
        {
            var data = Encoding.UTF8.GetBytes(msg);
            _stream.Write(data, 0, Math.Min(data.Length, BufferSize));
            Log($"Отправлено: {msg}");
        }

        public string Receive()
        {
            var buf = new byte[BufferSize];
            int cnt = _stream.Read(buf, 0, buf.Length);
            var res = Encoding.UTF8.GetString(buf, 0, cnt);
            Log($"Принято: {res}");
            return res;
        }

        private static void Log(string msg) =>
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}][CLIENT] {msg}");
    }
}
