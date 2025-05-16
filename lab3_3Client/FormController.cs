using lab3Client;

namespace lab3_3Client;

/// <summary>
/// Логика обмена с сервером и накопление точек (T, P)
/// </summary>
internal class FormController
{
    private Client? _client;
    private readonly List<double> _temps = new();
    private readonly List<double> _pressures = new();
    private readonly System.Windows.Forms.Timer _timer;

    public event Action<string>? Errors;
    public event Action<List<double>, List<double>>? DataUpdated;

    public FormController()
    {
        _timer = new System.Windows.Forms.Timer
        {
            Interval = 1_000   // 1 с
        };
        _timer.Tick += (_, __) => PollServer();
    }

    /* ---------- публичный API для формы ---------- */

    public void ConnectToServer(string ip)
    {
        try
        {
            Disconnect();                       // на всякий случай
            _client = new Client(ip);
            _client.Connect();
        }
        catch (Exception ex)
        {
            Errors?.Invoke($"Не удалось подключиться: {ex.Message}");
        }
    }

    public void StartGetData()
    {
        _temps.Clear();
        _pressures.Clear();
        _timer.Start();
    }

    public void Disconnect()
    {
        _timer.Stop();
        if (_client?.Connected == true)
            _client.Close();
    }

    public void ClearValues()
    {
        _temps.Clear();
        _pressures.Clear();
    }

    /* ---------- inner logic ---------- */

    private void PollServer()
    {
        try
        {
            if (_client?.Connected != true)
            {
                Errors?.Invoke("Соединение потеряно.");
                _timer.Stop();
                return;
            }

            string raw = _client.GetResponce();
            if (string.IsNullOrWhiteSpace(raw))
            {
                Errors?.Invoke("Пустой ответ от сервера.");
                return;
            }

            // ожидаем строку "T;P"
            string[] parts = raw.Split(';');
            if (parts.Length == 2 &&
                double.TryParse(parts[0], out double t) &&
                double.TryParse(parts[1], out double p))
            {
                _temps.Add(t);
                _pressures.Add(p);
                DataUpdated?.Invoke(_temps, _pressures);
            }
            else
            {
                Errors?.Invoke($"Неверный формат данных: {raw}");
            }
        }
        catch (Exception ex)
        {
            Errors?.Invoke($"Ошибка при получении данных: {ex.Message}");
            _timer.Stop();
        }
    }
}
