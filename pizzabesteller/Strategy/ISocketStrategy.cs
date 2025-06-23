namespace pizzabesteller.Strategy
{
    public interface ISocketStrategy
    {
        event Action<string> MessageReceived;

        public void SendMessage(string message);

        public void StartListening();

        void Stop();
    }
}