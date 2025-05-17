using lab3Client;
using System.IO;
using System.Net.Sockets;

namespace lab3Client
{
    internal class TranslatorController
    {
        private Client? _client;

        public Dictionary<string, string> DisplayNameToFullPath { get; } = new();

        // события
        public event EventHandler<string>? DirectoryChanged;
        public event EventHandler<string>? FileSelected;
        public event Action<string>? Errors;
        public event Action<string>? SocketError;

        #region Публичный API

        public string[] GetLogicalDrives() => Directory.GetLogicalDrives();

        public string[] GetDirectoryEntries(string path)
        {
            try
            {
                DisplayNameToFullPath.Clear();

                if (!Directory.Exists(path))
                    throw new DirectoryNotFoundException($"Каталог не найден: {path}");

                SafeSend(path);
                var entries = SafeReceive()
                             .Split('|', StringSplitOptions.RemoveEmptyEntries);

                foreach (var entry in entries)
                {
                    string display, fullPath;

                    switch (entry)
                    {
                        case ".":
                            display = ".";
                            fullPath = path;                      // текущий
                            break;

                        case "..":
                            display = "..";
                            fullPath = Directory.GetParent(path)?.FullName ?? path;
                            break;

                        default:
                            display = Path.GetFileName(entry);
                            if (string.IsNullOrWhiteSpace(display))
                                display = entry;
                            fullPath = entry;
                            break;
                    }

                    DisplayNameToFullPath.TryAdd(display, fullPath);
                }

                DirectoryChanged?.Invoke(this, path);
                return DisplayNameToFullPath.Keys.ToArray();
            }
            catch (IOException sockEx)
            {
                SocketError?.Invoke(sockEx.Message);
                return Array.Empty<string>();
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
                return Array.Empty<string>();
            }
        }

        public string GetFileText(string path)
        {
            SafeSend(path);
            return SafeReceive();
        }

        public void OnItemSelected(string displayName)
        {
            try
            {
                if (!DisplayNameToFullPath.TryGetValue(displayName, out var fullPath))
                    return;

                if (Directory.Exists(fullPath))
                    DirectoryChanged?.Invoke(this, fullPath);
                else if (File.Exists(fullPath))
                    FileSelected?.Invoke(this, fullPath);
            }
            catch (IOException sockEx)
            {
                SocketError?.Invoke(sockEx.Message);
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
            }
        }

        public string[] ConnectToServer(string ip)
        {
            try
            {
                Disconnect();                                 

                _client = new Client(ip);
                _client.Connect();

                return SafeReceive().Split(',');
            }
            catch (SocketException sockEx)
            {
                SocketError?.Invoke(sockEx.Message);
                return Array.Empty<string>();
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
                return Array.Empty<string>();
            }
        }

        public void Disconnect()
        {
            try
            {
                if (_client?.Connected == true)
                    _client.Close();
            }
            catch (IOException sockEx)
            {
                SocketError?.Invoke(sockEx.Message);
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
            }
            finally
            {
                _client = null;                               // чтобы можно было переподключаться
            }
        }

        #endregion
        /* ----------------------------------------------------------------- */
        #region Приватные хелперы

        private void SafeSend(string msg)
        {
            if (_client?.Connected != true)
                throw new InvalidOperationException("Нет активного соединения с сервером.");

            _client.Send(msg);
        }

        private string SafeReceive()
        {
            if (_client?.Connected != true)
                throw new InvalidOperationException("Нет активного соединения с сервером.");

            return _client.Receive();
        }

        #endregion
    }
}
