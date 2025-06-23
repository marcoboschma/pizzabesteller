using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using pizzabesteller.Factoryd;
using pizzabesteller.Strategy;

namespace pizzabesteller.uidUx
{
    public class NawPanel
    {
        public string Name;

        public string Adress;

        public string Postcode;

        public TextBox NaamBox
        {
            get; set;
        }

        public TextBox AddressBox
        {
            get; set;
        }

        public TextBox PosCodeBox
        {
            get; set;
        }

        public Button ConfirmButton
        {
            get; set;
        }

        public List<MenuItemModel> menuItemModels = new List<MenuItemModel>();

        private StackPanel stackPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Stretch,
            Background = new ImageBrush
            {
                Stretch = Stretch.Fill // or Stretch.Fill, depending on your desired effect
            }
        };

        public NawPanel()
        {
            NaamBox = TextBoxFactory.CreateTextBox("Enter Name", 300, 30, 10, HorizontalAlignment.Left);

            AddressBox = TextBoxFactory.CreateTextBox("Enter Adress", 300, 30, 10, HorizontalAlignment.Left);
            PosCodeBox = TextBoxFactory.CreateTextBox("Enter Postcode", 300, 30, 10, HorizontalAlignment.Left);

            ConfirmButton = ButtonFactory.CreateButton("Confirm ", 150, 30, 10, HorizontalAlignment.Left);

            ConfirmButton.Click += ConfirmButton_Click;

            NaamBox.KeyDown += NaamBox_KeyDown;
            AddressBox.KeyDown += AddressBox_KeyDown;
            PosCodeBox.KeyDown += PosCodeBox_KeyDown;
        }

        private void PosCodeBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Postcode = PosCodeBox.Text;
            }
        }

        private void AddressBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Adress = AddressBox.Text;
            }
        }

        private void NaamBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Name = NaamBox.Text;
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("confirm");
            Console.WriteLine(DateTime.Now.ToString());
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Adress) && !string.IsNullOrEmpty(Postcode))
            {
                Console.WriteLine("Sending" + Name + "\n" + Adress + "\n" + Postcode + "\n" + string.Join("", menuItemModels.Select(x => x.GetSandersEisen())) + DateTime.Now);

                SocketContext.Instance.SendMessage("SendBill" + "\n" + Name + "\n" + Adress + "\n" + Postcode + "\n" + string.Join("", menuItemModels.Select(x => x.GetSandersEisen())) + DateTime.Now);
            }
        }

        public void HidePanel()
        {
            stackPanel.Visibility = Visibility.Collapsed;
            for (int i = 0; i < stackPanel.Children.Count; i++)
            {
                stackPanel.Children[i].Visibility = Visibility.Hidden;
            }
        }

        public void ShowPanel()
        {
            stackPanel.Visibility = Visibility.Visible;

            for (int i = 0; i < stackPanel.Children.Count; i++)
            {
                stackPanel.Children[i].Visibility = Visibility.Visible;
            }
        }

        public StackPanel renderPanel()
        {
            stackPanel.Children.Add(NaamBox);
            stackPanel.Children.Add(AddressBox);
            stackPanel.Children.Add(PosCodeBox);
            stackPanel.Children.Add(ConfirmButton);
            return stackPanel;
        }
    }
}