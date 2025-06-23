using System.Windows;
using System.Windows.Controls;
using pizzabesteller.uidUx;
using pizzabesteller.Visitor;

namespace pizzabesteller
{
    public partial class MainWindow : Window
    {
        public ServerPanel ServerPanel;

        public DebugPanel DebugPanel = new DebugPanel();

        public MenuPanel MenuPanel = new MenuPanel();

        public ScrollViewer myScrollViewer = new ScrollViewer
        {
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
        };

        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.ServerPanel = new ServerPanel(MenuPanel);
            StackPanel temppanel = ServerPanel.RenderPanel();

            StackPanel orderTPanel = MenuPanel.RenderPanel();

            StackPanel deburePanel = (StackPanel)DebugPanel.Render();
            StackPanel GetConnectPanel = new StackPanel();

            //DebugPanel.Accept(new StyleVisitor());
            DebugPanel.Accept(new StyleBackGroundVisitorChoco());

            StackPanel panel = new StackPanel();

            panel.VerticalAlignment = VerticalAlignment.Stretch;
            panel.HorizontalAlignment = HorizontalAlignment.Stretch;
            panel.Children.Add(orderTPanel);
            panel.Children.Add(temppanel);
            panel.Children.Add(deburePanel);
            myScrollViewer.Content = panel;

            this.Content = myScrollViewer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //handler.StartClient("127.0.0.1", Commands.GetMenu());
        }

        //dit is de tcp knop voor load order
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void Stat_Initialized(object sender, EventArgs e)
        {
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}