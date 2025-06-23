namespace pizzabesteller.Strategy
{
    public class SocketContext
    {
        private static readonly SocketContext instance = new SocketContext();
        public ISocketStrategy _strategy;

        public event Action<string> MessageReceived;

        private SocketContext()
        {
        } // Private constructor for Singleton

        public static SocketContext Instance => instance;

        public void SetStrategy(ISocketStrategy strategy)
        {
            _strategy = strategy;
            _strategy.MessageReceived += OnMessageReceived;
        }

        public void SendMessage(string message)
        {
            _strategy?.SendMessage(message);
        }

        public void StartListening()
        {
            _strategy?.StartListening();
        }

        public void Stop()
        {
            _strategy?.Stop();
        }

        private void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(message);
        }
    }
}