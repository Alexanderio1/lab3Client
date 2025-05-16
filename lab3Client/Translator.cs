using lab3;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3Client
{
    public partial class Translator : Form
    {
        private readonly TranslatorController _controller = new();
        private readonly Server _server = new();

        public Translator()
        {
            InitializeComponent();

            // подписка на события контроллера
            _controller.DirectoryChanged += HandleDirectoryChanged;
            _controller.FileSelected += HandleFileSelected;
            _controller.Errors += ShowError;
            _controller.SocketError += ShowSocketError;

            this.Shown += Translator_Shown;


            // запускаем сервер в фоновом потоке
            Task.Run(() => _server.Start());

            buttonDisconnect.Enabled = false;
            AppendClientLog("Клиентское приложение запущено");
        }

        // ------------------------- обработка изменённого текста пути -------------------------
        private void comboBoxSearch_TextChanged(object sender, EventArgs e)
        {
            var path = comboBoxSearch.Text.Trim();
            if (string.IsNullOrEmpty(path)) return;

            WithBusyCursor(() =>
            {
                _controller.GetDirectoryEntries(path);
                fileContent.Clear();
            });
        }
        private void Translator_Shown(object sender, EventArgs e)
        {
            // теперь форма полностью создана, и richTextServer.Handle != IntPtr.Zero 
            _server.OnLog += AppendServerLog;
            Task.Run(() => _server.Start());
        }

        // двойной клик по элементу списка
        private void SendToServer_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxSearch.SelectedItem == null) return;
            var displayName = listBoxSearch.SelectedItem.ToString();
            _controller.OnItemSelected(displayName);
        }

        #region Events from controller --------------------------------------------------
        private void HandleDirectoryChanged(object? sender, string path)
        {
            try
            {
                if (!comboBoxSearch.Items.Contains(path))
                    comboBoxSearch.Items.Add(path);

                comboBoxSearch.Text = path;

                listBoxSearch.BeginUpdate();
                listBoxSearch.Items.Clear();
                listBoxSearch.Items.AddRange(_controller.DisplayNameToFullPath.Keys.ToArray());
                listBoxSearch.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении списка: {ex.Message}");
            }
        }

        private void HandleFileSelected(object? sender, string filePath)
        {
            AppendClientLog($"Клиент запросил файл: {filePath}");
            WithBusyCursor(() => fileContent.Text = _controller.GetFileText(filePath));
            AppendClientLog($"Клиент получил файл (длина {fileContent.Text.Length} символов)");
        }
        #endregion

        #region Buttons ----------------------------------------------------------------
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            WithBusyCursor(() =>
            {
                ClearForm();
                var drives = _controller.ConnectToServer(textBoxIPAddress.Text);
                comboBoxSearch.Items.AddRange(drives);

                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
            });

            AppendClientLog($"Клиент подключился к {textBoxIPAddress.Text}");
        }

        private void buttonRequestDir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxSearch.Text))
            {
                MessageBox.Show("Введите путь каталога.");
                return;
            }

            var path = comboBoxSearch.Text.Trim();
            AppendClientLog($"Клиент запросил структуру каталога: {path}");

            WithBusyCursor(() =>
            {
                var entries = _controller.GetDirectoryEntries(path);
                listBoxSearch.Items.Clear();
                listBoxSearch.Items.AddRange(entries);
                fileContent.Clear();
                AppendClientLog($"Клиент получил структуру каталога ({entries.Length} элементов)");
            });
        }

        private void buttonRequestFile_Click(object sender, EventArgs e)
        {
            if (listBoxSearch.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите элемент в списке.");
                return;
            }

            var name = listBoxSearch.SelectedItem.ToString();
            if (!_controller.DisplayNameToFullPath.TryGetValue(name, out var path) || !File.Exists(path))
            {
                MessageBox.Show("Выбранный элемент не является файлом.");
                return;
            }

            AppendClientLog($"Клиент запросил файл: {path}");
            WithBusyCursor(() => fileContent.Text = _controller.GetFileText(path));
            AppendClientLog($"Клиент получил файл (длина {fileContent.Text.Length} символов)");
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            _controller.Disconnect();
            ClearForm();
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            AppendClientLog("Клиент отключился от сервера");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            _controller.Disconnect();
            _server.Stop();
            AppendClientLog("Клиентское приложение закрывается");
            Close();
        }
        #endregion

        private void Translator_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controller.Disconnect();
            _server.Stop();
            AppendClientLog("Форма закрыта, соединение разорвано");
        }

        #region UI helpers --------------------------------------------------------------
        private void ShowError(string msg) => MessageBox.Show($"Error: {msg}");

        private void ShowSocketError(string msg)
        {
            MessageBox.Show($"Socket error: {msg}");
            _controller.Disconnect();
            ClearForm();
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            AppendClientLog("Произошла ошибка сокета, соединение разорвано");
        }

        private void AppendServerLog(string text)
        {
            if (richTextServer.InvokeRequired)
                richTextServer.BeginInvoke(new Action(() => AppendServerLog(text)));
            else
                richTextServer.AppendText(text + Environment.NewLine);
        }


        private void AppendClientLog(string text)
        {
            var line = $"[{DateTime.Now:HH:mm:ss}] {text}{Environment.NewLine}";
            if (richTextClient.InvokeRequired)
            {
                richTextClient.BeginInvoke(new Action(() => richTextClient.AppendText(line)));
                return;
            }
            richTextClient.AppendText(line);
        }

        private void ClearForm()
        {
            listBoxSearch.Items.Clear();
            fileContent.Clear();
            comboBoxSearch.Items.Clear();
            comboBoxSearch.Text = string.Empty;
        }

        private void WithBusyCursor(Action action)
        {
            Cursor = Cursors.WaitCursor;
            try { action(); }
            finally { Cursor = Cursors.Default; }
        }
        #endregion
    }
}
