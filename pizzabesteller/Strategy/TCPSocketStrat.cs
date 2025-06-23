using System.IO;
using System.Net.Sockets;
using pizzabesteller.connection;

namespace pizzabesteller.Strategy
{
    public class TCPSocketStrat : ISocketStrategy
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private Thread _listenerThread;

        public event Action<string> MessageReceived;

        private bool _isListening = false;

        public void SendMessage(string message)
        {
            try
            {
                // Connect only if not already connected
                if (_client == null || !_client.Connected)
                {
                    _client = new TcpClient("127.0.0.1", 13000); // Adjust host/port as needed
                    _stream = _client.GetStream();
                    StartListening();
                }

                Console.WriteLine("Sending this with TCP: " + message);
                byte[] encrypted = AesEncryptionWithPassphrase.Encrypt(message, "plop");
                _stream.Write(encrypted, 0, encrypted.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TCP Sendmsg: {ex.Message}");
            }
        }

        public void Stop()
        {
            _stream?.Close();
            _client?.Close();
            Console.WriteLine("TCP connection stopped.");
        }

        public void Sendmsg(string message)
        {
            try
            {
                // Connect only if not already connected
                if (_client == null || !_client.Connected)
                {
                    _client = new TcpClient("127.0.0.1", 13000); // Adjust host/port as needed
                    _stream = _client.GetStream();
                    StartListening();
                }

                Console.WriteLine("Sending this with TCP: " + message);
                byte[] encrypted = AesEncryptionWithPassphrase.Encrypt(message, "plop");
                _stream.Write(encrypted, 0, encrypted.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TCP Sendmsg: {ex.Message}");
            }
        }

        private void StartListening()
        {
            if (_isListening)
            {
                return;
            }

            _isListening = true;
            _listenerThread = new Thread(Listen)
            {
                IsBackground = true
            };
            _listenerThread.Start();
        }

        private void Listen()
        {
            Console.WriteLine("Listening on TCP...");
            try
            {
                var buffer = new byte[1024];

                while (_client.Connected)
                {
                    Console.WriteLine("still listning");
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        byte[] data = new byte[bytesRead];
                        Array.Copy(buffer, 0, data, 0, bytesRead);

                        string decrypted = AesEncryptionWithPassphrase.Decrypt(data, "plop");
                        Console.WriteLine($"{decrypted}");
                        MessageReceived?.Invoke(decrypted);
                    }
                    else
                    {
                        // Client disconnected
                        break;
                    }
                }
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"TCP stream closed: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while listening to TCP: {ex.Message}");
            }
            finally
            {
                _isListening = false;
                _stream?.Close();
                _client?.Close();
            }
        }

        void ISocketStrategy.StartListening() => StartListening();
    }
}