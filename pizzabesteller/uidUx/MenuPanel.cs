using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using pizzabesteller.Factoryd;

namespace pizzabesteller.uidUx
{
    public class MenuPanel
    {
        private List<MenuItemModel> _menuItems = new();

        public List<string> ExtraIngrediants = new List<string>();
        public bool showMenu = false;

        public TextBox Title
        {
            get; private set;
        }

        public TextBox Description
        {
            get; private set;
        }

        public Button OpenMenu
        {
            get; private set;
        }

        public TextBox MenuDescription
        {
            get; private set;
        }

        public TextBox MenuContents
        {
            get; private set;
        }

        public Button RefreshMenuButton
        {
            get; private set;
        }

        public ListBox MenuList
        {
            get; private set;
        }

        public ListBox ExtraIngredientsList
        {
            get; private set;
        }

        public TextBox SelectedItemDetails
        {
            get; private set;
        }

        public TextBox IngredientInput
        {
            get; private set;
        }

        public Button AddIngredientButton
        {
            get; private set;
        }

        public Button AddItemToBasketButton
        {
            get; private set;
        }

        public Button RemoveItemFromBasketButton
        {
            get; private set;
        }

        public Button EnterNawStuffButton
        {
            get; private set;
        }

        public NawPanel nawPanel = new NawPanel();

        private StackPanel stackPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            Background = new ImageBrush
            {
                Stretch = Stretch.Fill // or Stretch.Fill, depending on your desired effect
            }
        };

        public MenuPanel()
        {
            Title = TextBoxFactory.CreateTextBox("MenuPanel", 300, 30, 10, HorizontalAlignment.Left);
            //Description = TextBoxFactory.CreateTextBox("Pizza Order Tba", 300, 60, 10, HorizontalAlignment.Left, TextWrapping.Wrap);

            OpenMenu = ButtonFactory.CreateButton("Open menu", 150, 30, 10, HorizontalAlignment.Left);
            OpenMenu.Click += OpenMenu_Click;

            MenuList = new ListBox
            {
                Width = 300,
                Height = 150,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            ExtraIngredientsList = new ListBox
            {
                Width = 300,
                Height = 150,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            MenuList.SelectionChanged += OnMenuItemSelected;

            SelectedItemDetails = TextBoxFactory.CreateTextBox("Select an item to view ingredients", 300, 60, 10, HorizontalAlignment.Left, TextWrapping.Wrap);
            //IngredientInput = TextBoxFactory.CreateTextBox("Enter extra ingredient", 300, 30, 10, HorizontalAlignment.Left);
            AddIngredientButton = ButtonFactory.CreateButton("Add Ingredient", 150, 30, 10, HorizontalAlignment.Left);
            AddIngredientButton.Click += OnAddIngredient;

            AddItemToBasketButton = ButtonFactory.CreateButton("Add ToBasket", 150, 30, 10, HorizontalAlignment.Left);
            RemoveItemFromBasketButton = ButtonFactory.CreateButton("Remove To Basket", 150, 30, 10, HorizontalAlignment.Left);

            AddItemToBasketButton.Click += AddItemToBasketButton_Click;
            RemoveItemFromBasketButton.Click += RemoveItemFromBasketButton_Click;

            EnterNawStuffButton = ButtonFactory.CreateButton("Confirm and Order", 400, 30, 10, HorizontalAlignment.Left);
            EnterNawStuffButton.Click += EnterNawStuffButton_Click;

            MenuDescription = TextBoxFactory.CreateTextBox("Food Menu Description", 300, 30, 10, HorizontalAlignment.Left);
            //MenuContents = TextBoxFactory.CreateTextBox("Menu will appear here", 300, 150, 10, HorizontalAlignment.Left, TextWrapping.Wrap);
            RefreshMenuButton = ButtonFactory.CreateButton("Load Menu", 150, 30, 10, HorizontalAlignment.Left);
            RefreshMenuButton.Click += (s, e) =>
            {
                UpdateText(MenuDescription, "Loading today's menu...");
            };

            nawPanel.HidePanel();
        }

        private void EnterNawStuffButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Order confirmed.");

            List<MenuItemModel> selectedItems = _menuItems.FindAll(item => item.Quantity > 0);
            nawPanel.menuItemModels = selectedItems;
            nawPanel.ShowPanel();
        }

        private void OpenMenu_Click(object sender, RoutedEventArgs e)
        {
            showMenu = !showMenu;

            foreach (var item in stackPanel.Children)
            {
                if (item is TextBox box)
                {
                    box.Visibility = showMenu ? Visibility.Visible : Visibility.Hidden;
                }

                if (item is ListBox lbox)
                {
                    lbox.Visibility = showMenu ? Visibility.Visible : Visibility.Hidden;
                }

                if (item is Button button && button != OpenMenu)
                {
                    button.Visibility = showMenu ? Visibility.Visible : Visibility.Hidden;
                }
            }
        }

        public void OpenMenuFUN()
        {
            nawPanel.HidePanel();

            foreach (var item in stackPanel.Children)
            {
                if (item is FrameworkElement fe)
                {
                    fe.Visibility = showMenu || item is Button b && b == OpenMenu
                        ? Visibility.Visible
                        : Visibility.Hidden;
                }
            }
        }

        private void RemoveItemFromBasketButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuList.SelectedItem is not MenuItemModel selectedItem)
            {
                return;
            }

            selectedItem.RemoveFromBasket();
        }

        private void AddItemToBasketButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuList.SelectedItem is not MenuItemModel selectedItem)
            {
                return;
            }

            selectedItem.AddToBasket();
            Console.WriteLine("adding to basket");
        }

        public void UpdateText(TextBox target, string text)
        {
            Application.Current.Dispatcher.Invoke(() => target.Text = text);
        }

        public void LoadMenuItems(List<MenuItemModel> menuItems)
        {
            _menuItems.AddRange(menuItems);

            foreach (var item in _menuItems)
            {
                MenuList.Items.Add(item);
            }
            ExtraIngrediants.AddRange(new MenuParser().GetAllIngrediants(menuItems));

            foreach (var item in ExtraIngrediants)
            {
                ExtraIngredientsList.Items.Add(item);
            }
        }

        private void OnMenuItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (MenuList.SelectedItem is MenuItemModel selectedItem)
            {
                UpdateText(SelectedItemDetails, selectedItem.GetFullDescription());

                AddItemToBasketButton.Visibility = Visibility.Visible;
            }
        }

        private void OnAddIngredient(object sender, RoutedEventArgs e)
        {
            // Ensure a menu item is selected.
            if (!(MenuList.SelectedItem is MenuItemModel selectedItem))
            {
                UpdateText(SelectedItemDetails, "Please select a menu item first.");
                return;
            }

            // Ensure an extra ingredient is selected from the list.
            if (!(ExtraIngredientsList.SelectedItem is string selectedExtraIngredient))
            {
                UpdateText(SelectedItemDetails, "Please select an extra ingredient from the list.");
                return;
            }

            // Add the selected extra ingredient to the menu item.
            selectedItem.AddExtraIngredient(selectedExtraIngredient);
            UpdateText(SelectedItemDetails, selectedItem.GetFullDescription());
        }

        public StackPanel RenderPanel()
        {
            stackPanel.Children.Add(Title);
            stackPanel.Children.Add(OpenMenu);
            stackPanel.Children.Add(MenuDescription);
            stackPanel.Children.Add(RefreshMenuButton);
            stackPanel.Children.Add(new TextBlock
            {
                Text = "Food Menu",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10, 0, 0, 5)
            });
            stackPanel.Children.Add(MenuList);
            stackPanel.Children.Add(ExtraIngredientsList);
            stackPanel.Children.Add(SelectedItemDetails);
            stackPanel.Children.Add(AddIngredientButton);
            stackPanel.Children.Add(AddItemToBasketButton);
            stackPanel.Children.Add(RemoveItemFromBasketButton);
            stackPanel.Children.Add(EnterNawStuffButton);

            stackPanel.Children.Add(nawPanel.renderPanel()); // Add it somewhere visible
            OpenMenuFUN();

            return stackPanel;
        }
    }
}