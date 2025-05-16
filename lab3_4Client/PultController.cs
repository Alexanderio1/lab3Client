using System.Drawing;

namespace lab3_4Client
{
    internal class PultController
    {
        private Client? _client;
        private readonly System.Windows.Forms.Timer _timer;

        private int _unitCount;
        private List<Color> _unitColors = new();

        /* ---- события для формы ---- */
        public event Action<string>? Errors;
        public event Action<IReadOnlyList<Color>>? DataUpdated;

        public PultController()
        {
            _timer = new System.Windows.Forms.Timer
            {
                Interval = 2000                                // 2 с как на сервере
            };
            _timer.Tick += Timer_Tick;
        }

        /* ---------- Connect ---------- */
        public int ConnectToServer(string ip)
        {
            try
            {
                // 1) полностью выключаем прошлое соединение/таймер
                Disconnect();

                // 2) заводим новый клиент
                _client = new Client(ip);
                _client.Connect();

                _unitCount = int.Parse(_client.GetResponce());
                _unitColors = Enumerable.Repeat(Color.Gray, _unitCount).ToList();

                return _unitCount;
            }
            catch (Exception ex)
            {
                Errors?.Invoke($"Не удалось подключиться: {ex.Message}");
                return 0;
            }
        }

        /* ---------- Disconnect ---------- */
        public void Disconnect()
        {
            _timer.Stop();              // останавливаем без условий

            if (_client != null)
            {
                try { _client.Close(); }
                catch { /* игнорируем, сокет мог уже умереть */ }
            }

            _client = null;         // важный момент!
            _unitCount = 0;
            _unitColors = new List<Color>();
        }


        /* ---------- запуск опроса ---------- */
        public void StartGetData()
        {
            if (_unitCount == 0 || _client?.Connected != true)
            {
                Errors?.Invoke("Нет соединения с сервером.");
                return;
            }
            _timer.Start();            // каждый тик сам пошлёт ?Ready
        }

        /* ---------- таймер ---------- */
        private void Timer_Tick(object? _, EventArgs __)
        {
            if (_client?.Connected != true)
            {
                Errors?.Invoke("Соединение потеряно.");
                _timer.Stop();
                return;
            }

            try
            {
                // 1) просим очередную порцию
                _client.SendRequest("?Ready");

                // 2) ждём ответ (блокирующее чтение на пару сотен мс не критично)
                string data = _client.GetResponce();
                if (string.IsNullOrWhiteSpace(data))
                    return;                         // сервер не успел – попробуем через 2 с

                // 3) разбираем
                string[] states = data.Split(';');
                if (states.Length != _unitCount)
                {
                    Errors?.Invoke("Получен некорректный пакет от сервера.");
                    return;
                }

                for (int i = 0; i < _unitCount; i++)
                {
                    _unitColors[i] = states[i] switch
                    {
                        "WORKING" => Color.LimeGreen,
                        "FAILURE" => Color.Red,
                        "REPAIR" => Color.DimGray,
                        _ => Color.Black      // неизвестное состояние
                    };
                }

                DataUpdated?.Invoke(_unitColors);
            }
            catch (Exception ex)
            {
                Errors?.Invoke($"Ошибка обмена: {ex.Message}");
                Disconnect();
            }
        }

        /* ---------- сброс для кнопки 'Сбросить' ---------- */
        public void ClearValues()
        {
            _unitColors = Enumerable.Repeat(Color.Gray, _unitCount).ToList();
            DataUpdated?.Invoke(_unitColors);
        }
    }
}
