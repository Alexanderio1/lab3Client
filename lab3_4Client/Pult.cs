using System.Drawing;
using System.Windows.Forms;

namespace lab3_4Client
{
    public partial class Pult : Form
    {
        private readonly PultController _controller = new();
        private readonly List<Button> _buttons = new();

        public Pult()
        {
            InitializeComponent();

            _controller.Errors += ShowError;
            _controller.DataUpdated += UpdateButtons;
        }

        /* ---------- UI helpers ---------- */
        private void ShowError(string msg) =>
            MessageBox.Show(this, msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void UpdateButtons(IReadOnlyList<Color> colors)
        {
            // гарантируем вызов из UI-потока
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateButtons(colors)));
                return;
            }

            for (int i = 0; i < colors.Count && i < _buttons.Count; i++)
                _buttons[i].BackColor = colors[i];
        }

        /* ---------- динамическое создание кнопок ---------- */
        private void CreateButtons(int count)
        {
            // очистить предыдущие (если переподключаемся)
            foreach (var b in _buttons)
                splitContainer1.Panel1.Controls.Remove(b);
            _buttons.Clear();

            if (count == 0) return;

            const int size = 100;
            const int pad = 10;
            const int perRow = 10;

            for (int i = 0; i < count; i++)
            {
                var btn = new Button
                {
                    Text = $"Установка {i + 1}",
                    Font = new Font("Segoe UI", 9f),
                    Width = size,
                    Height = size,
                    BackColor = Color.Green,
                    Location = new Point(
                        pad + (size + pad) * (i % perRow),
                        pad + (size + pad) * (i / perRow))
                };
                _buttons.Add(btn);
                splitContainer1.Panel1.Controls.Add(btn);
            }
        }

        /* ---------- кнопки управления ---------- */
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            int n = _controller.ConnectToServer(textBoxIPAddress.Text.Trim());
            CreateButtons(n);
            _controller.StartGetData();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e) =>
            _controller.Disconnect();
    }
}
