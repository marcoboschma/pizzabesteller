using System.Windows;
using pizzabesteller.Visitor;

namespace pizzabesteller.uidUx
{
    // Component Interface
    //voor visitor
    public interface IGUIComponent
    {
        UIElement Render();

        void Accept(IGUIComponentVisitor visitor);
    }
}