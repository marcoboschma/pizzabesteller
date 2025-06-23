using System.Windows;
using System.Windows.Controls;
using pizzabesteller.Visitor;

namespace pizzabesteller.uidUx
{
    public class DebugButton : IGUIComponent
    {
        private Button button;

        public DebugButton(string label, RoutedEventHandler onClick)
        {
            button = new Button
            {
                Content = label,
                Width = 100,
                Height = 30,
                Margin = new Thickness(5)
            };
            button.Click += onClick;
        }

        public UIElement Render()
        {
            return button;
        }

        public void Accept(IGUIComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}