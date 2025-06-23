using System.Windows;
using System.Windows.Controls;
using pizzabesteller.Factoryd;
using pizzabesteller.Visitor;

namespace pizzabesteller.uidUx
{
    // Each concrete element class must implement the `accept`
    // method in such a way that it calls the visitor's method that
    // corresponds to the element's class.
    public class DebugTextBox : IGUIComponent
    {
        private TextBox textBox;

        public DebugTextBox(string text)
        {
            textBox = TextBoxFactory.CreateTextBox(text, 300, 30, 10, HorizontalAlignment.Left);
        }

        public UIElement Render()
        {
            return textBox;
        }

        public void Accept(IGUIComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void SetText(string log)
        {
            textBox.Text = log;
        }
    }
}