using System.Windows;
using System.Windows.Controls;
using pizzabesteller.Visitor;

namespace pizzabesteller.uidUx
{
    // Composite: DebugPanel
    public class DebugPanel : IGUIComponent
    {
        private List<IGUIComponent> components = new List<IGUIComponent>();
        private StackPanel stackPanel;
        private DebugTextBox debugText;

        public DebugPanel()
        {
            stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            components.Add(new DebugTextBox("Debugger"));
            components.Add(new DebugTextBox("Info"));
            debugText = new DebugTextBox("DebugInfo");
            components.Add(debugText);

            components.Add(new DebugButton("Clear", (s, e) => LogToPanel("Cleared!")));
            components.Add(new DebugButton("Test", (s, e) => LogToPanel("Test Button Clicked!")));
        }

        public UIElement Render()
        {
            stackPanel.Children.Clear();
            foreach (IGUIComponent component in components)
            {
                stackPanel.Children.Add(component.Render());
            }
            return stackPanel;
        }

        public void LogToPanel(string log)
        {
            debugText.SetText(log);
        }

        public void ClearPanel(ref Grid grid)
        {
            grid.Children.Remove(stackPanel);
        }

        public void Accept(IGUIComponentVisitor visitor)
        {
            foreach (IGUIComponent component in components)
            {
                component.Accept(visitor);
            }
        }
    }
}