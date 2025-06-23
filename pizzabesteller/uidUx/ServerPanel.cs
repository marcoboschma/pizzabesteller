using System.Windows;
using System.Windows.Controls;
using pizzabesteller.Factoryd;
using pizzabesteller.Strategy;

namespace pizzabesteller.uidUx
{
    public class ServerPanel : IServerPanelTemp
    {
        public TextBox Title
        {
            get; private set;
        }

        public TextBox Description
        {
            get; private set;
        }

        public TextBox Messagelog
        {
            get; set;
        }

        public Button ConnectToServer
        {
            get; private set;
        }

        public Button UseUDPbtn
        {
            get; private set;
        }

        public Button UseTCPbtn
        {
            get; private set;
        }

        public bool ShowConnectionTypes = false;

        //private readonly INetworkHandler _networkHandler;

        public bool isConnected
        {
            get; private set;
        }

        public MenuPanel MenuPanel
        {
            get; private set;
        }

        private StackPanel stackPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        public ServerPanel(MenuPanel menuPanel)
        {
            this.MenuPanel = menuPanel;
            this.MenuPanel.showMenu = false;
            isConnected = false;
            // Initialize the Title TextBox
            SocketContext.Instance.MessageReceived += OnMessageReceived;

            Title = TextBoxFactory.CreateTextBox("Serverpanel", 300, 30, 10, HorizontalAlignment.Left);

            // Initialize the Description TextBox
            Description = TextBoxFactory.CreateTextBox("Selection for conn", 300, 60, 10, HorizontalAlignment.Left, TextWrapping.Wrap);

            Messagelog = TextBoxFactory.CreateTextBox("ServerLogs", 300, 60, 10, HorizontalAlignment.Left, TextWrapping.Wrap);

            // Initialize the ConnectToServer Button
            ConnectToServer = ButtonFactory.CreateButton("Show COnnection options", 150, 30, 10, HorizontalAlignment.Left);
            ConnectToServer.Click += ConnectToServer_Click;

            UseTCPbtn = ButtonFactory.CreateButton("Use Tcp", 150, 30, 10, HorizontalAlignment.Left);
            UseTCPbtn.Click += UseTcp_Click;

            UseUDPbtn = ButtonFactory.CreateButton("Use UDP", 150, 30, 10, HorizontalAlignment.Left);
            UseUDPbtn.Click += UseUDp_Click;

            UseUDPbtn.Visibility = Visibility.Hidden;
            UseTCPbtn.Visibility = Visibility.Hidden;
        }

        private void ConnectToServer_Click(object sender, RoutedEventArgs e)
        {
            this.ShowConnectionTypes = true;
            this.Showoptions();

            ConnectToServer.Visibility = Visibility.Hidden;
        }

        //send NAw from here

        private void Showoptions()
        {
            UseUDPbtn.Visibility = Visibility.Visible;
            UseTCPbtn.Visibility = Visibility.Visible;
        }

        private void UseUDp_Click(object sender, RoutedEventArgs e)
        {
            //UseUDPbtn.Visibility = Visibility.Hidden;
            UpdateText(Description, "Using Udp");
            SocketContext.Instance.SetStrategy(new UdpSocketStrat());
            SocketContext.Instance.SendMessage(Commands.GetMenu());
            SocketContext.Instance.MessageReceived += OnMessageReceived;
            OnConnected();

            UseTCPbtn.Visibility = Visibility.Hidden;
        }

        public void OnConnected()
        {
            isConnected = true;
            this.MenuPanel.showMenu = true;
            this.MenuPanel.OpenMenuFUN();
        }

        private void UseTcp_Click(object sender, RoutedEventArgs e)
        {
            UseTCPbtn.Visibility = Visibility.Hidden;
            UpdateText(Description, "Using Tcp");
            SocketContext.Instance.SetStrategy(new TCPSocketStrat());
            SocketContext.Instance.SendMessage(Commands.GetMenu());
            SocketContext.Instance.MessageReceived += OnMessageReceived;
            OnConnected();

            UseUDPbtn.Visibility = Visibility.Hidden;
        }

        private void OnMessageReceived(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Console.WriteLine("Got this" + message);
                new MsgParser().ParseMsg(MenuPanel, message);
                Messagelog.Text += $"{message}\n";
            });
        }

        private void UpdateText(TextBox box, string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                box.Text += $"{message}\n";
            });
        }

        //onderdeel van builder

        public StackPanel RenderPanel()
        {
            // Add the controls to the StackPanel
            stackPanel.Children.Add(Title);
            stackPanel.Children.Add(Description);
            stackPanel.Children.Add(ConnectToServer);
            stackPanel.Children.Add(Messagelog);
            stackPanel.Children.Add(UseTCPbtn);
            stackPanel.Children.Add(UseUDPbtn);
            return stackPanel;
        }
    }
}