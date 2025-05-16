using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab3_2Client
{
    internal class FormController
    {
        private Client client = null!;
        private readonly System.Windows.Forms.Timer timer;
        private readonly List<double> temps = new();
        private readonly List<double> pressures = new();

        // Флаг, что мы уже уведомили о потере соединения
        private bool _notifiedConnectionLost = false;

        public event Action<string>? Errors;
        public event Action<List<double>, List<double>>? DataUpdated;

        public FormController()
        {
            timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Подключается к серверу, сбрасывает флаг потери соединения.
        /// </summary>
        public void ConnectToServer(string ip)
        {
            try
            {
                Disconnect();
                client = new Client(ip);
                client.Connect();
                _notifiedConnectionLost = false;   // сброс флага — мы снова «онлайн»
            }
            catch (Exception ex)
            {
                Errors?.Invoke($"Не удалось подключиться: {ex.Message}");
            }
        }

        /// <summary>
        /// Запускает сбор данных (и очищает старые точки).
        /// </summary>
        public void StartGetData()
        {
            temps.Clear();
            pressures.Clear();
            timer.Start();
        }

        /// <summary>
        /// Останавливает опрос и закрывает сокет.
        /// </summary>
        public void Disconnect()
        {
            timer.Stop();
            if (client?.Connected == true)
                client.Close();
        }

        /// <summary>
        /// Просто стирает накопленные точки (не трогает соединение).
        /// </summary>
        public void ClearValues()
        {
            temps.Clear();
            pressures.Clear();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            try
            {
                // если вдруг коннект порвался
                if (client?.Connected != true)
                {
                    // показываем эту ошибку только один раз
                    if (!_notifiedConnectionLost)
                    {
                        Errors?.Invoke("Соединение потеряно.");
                        _notifiedConnectionLost = true;
                    }
                    timer.Stop();
                    return;
                }

                string data = client.GetResponce();
                if (string.IsNullOrWhiteSpace(data))
                {
                    Errors?.Invoke("Пустой ответ от сервера.");
                    return;
                }

                var parts = data.Split(';');
                if (parts.Length == 2
                    && double.TryParse(parts[0], out double t)
                    && double.TryParse(parts[1], out double p))
                {
                    temps.Add(t);
                    pressures.Add(p);
                    DataUpdated?.Invoke(temps, pressures);
                }
                else
                {
                    Errors?.Invoke($"Неверный формат данных: {data}");
                }
            }
            catch (Exception ex)
            {
                // при исключении тоже не спамим
                if (!_notifiedConnectionLost)
                {
                    Errors?.Invoke($"Ошибка при получении данных: {ex.Message}");
                    _notifiedConnectionLost = true;
                }
                timer.Stop();
            }
        }
    }
}
