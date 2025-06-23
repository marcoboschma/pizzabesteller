using System.Net;
using System.Net.Sockets;
using pizzabesteller.connection;

namespace pizzabesteller.Strategy
{
    public class UdpSocketStrat : ISocketStrategy
    {
        private UdpClient _udpClient;
        private Thread _listenerThread;
        private CancellationTokenSource _cancellationTokenSource;

        public event Action<string> MessageReceived;

        public void SendMessage(string message)
        {
            try
            {
                if (_udpClient == null)
                {
                    _udpClient = new UdpClient();
                }
                //_udpClient = new UdpClient();
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Loopback, 13000);

                byte[] secData = AesEncryptionWithPassphrase.Encrypt(message, "plop");

                _udpClient.Send(secData, secData.Length, localEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending UDP message: {ex.Message}");
            }
            finally
            {
                //_udpClient?.Close();
                if (_listenerThread == null)
                {
                    StartListening();
                }
            }
        }

        public void StartListening()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _listenerThread = new Thread(() => Listen())
            {
                IsBackground = true
            };
            _listenerThread.Start();
        }

        //run dit maar een keer
        public void Listen()
        {
            _listenerThread = new Thread(() =>
            {
                UdpClient dpClient = new UdpClient(13001);
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 13001);

                while (true)
                {
                    try
                    {
                        byte[] receivedBytes = dpClient.Receive(ref remoteEndPoint);
                        string msg = AesEncryptionWithPassphrase.Decrypt(receivedBytes, "plop");
                        MessageReceived?.Invoke(msg);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in UDP listener: {ex.Message}");
                    }
                }
            })
            {
                IsBackground = true
            };
            _listenerThread.Start();
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            Console.WriteLine("UDP listener stopped.");
        }
    }
}