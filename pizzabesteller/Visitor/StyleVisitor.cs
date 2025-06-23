using System.Windows.Controls;
using pizzabesteller.uidUx;

namespace pizzabesteller.Visitor
{
    //visitor

    // Concrete visitors implement several versions of the same
    // algorithm, which can work with all concrete element classes.
    //
    // You can experience the biggest benefit of the Visitor pattern
    // when using it with a complex object structure such as a
    // Composite tree. In this case, it might be helpful to store
    // some intermediate state of the algorithm while executing the
    // visitor's methods over various objects of the structure.
    public class StyleVisitor : IGUIComponentVisitor
    {
        public void Visit(DebugTextBox textBox)
        {
            ((TextBox)textBox.Render()).Background = System.Windows.Media.Brushes.LightGray;
        }

        public void Visit(DebugButton button)
        {
            ((Button)button.Render()).Background = System.Windows.Media.Brushes.LightBlue;
        }
    }

    public class BingChilingVitor : IGUIComponentVisitor
    {
        public void Visit(DebugTextBox textBox)
        {
            throw new NotImplementedException();
        }

        public void Visit(DebugButton button)
        {
            throw new NotImplementedException();
        }
    }

    public class StyleBackGroundVisitorChoco : IGUIComponentVisitor
    {
        public void Visit(DebugTextBox textBox)
        {
            ((TextBox)textBox.Render()).Background = System.Windows.Media.Brushes.Chocolate;
        }

        public void Visit(DebugButton button)
        {
            ((Button)button.Render()).Background = System.Windows.Media.Brushes.Chocolate;
        }
    }
}