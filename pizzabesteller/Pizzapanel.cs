using System.Windows;
using System.Windows.Controls;
using pizzabesteller.utility;
using PizzaCaseInteractions;

namespace pizzabesteller
{
    public class Pizzapanel
    {
        public Grid Panel = new Grid();

        public PizzaOrdermenu ordermenu;

        public TextBox PizzaName;

        public TextBox PizzaPanelDiscription = new TextBox();

        public ListBox Pizzas = new ListBox();

        public Pizzapanel()
        {
            Panel panel = new Grid();

            ordermenu = new PizzaOrdermenu("Main", 1);
            ordermenu.AddToMenu(new PreMadePizza().tonno);
            ordermenu.AddToMenu(new PreMadePizza().kaas);

            PizzaPanelDiscription.Text = "pizza menu";
            panel.Children.Add(PizzaPanelDiscription);
        }

        public void SetupPizzaList()
        {
            foreach (var pizza in ordermenu.orderItems)
            {
                var litem = new ListBoxItem
                {
                    Content = pizza.Name // Set the content to the pizza name
                };
                Pizzas.Items.Add(litem);
            }
            Panel.Children.Add(Pizzas);
        }

        private StackPanel stackPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        public void AddElemets(UIElement element)
        {
            stackPanel.Children.Add(element);
        }

        public StackPanel RenderPanel()
        {
            return stackPanel;
        }

        public Grid GetGrid()
        {
            return Panel;
        }
    }
}