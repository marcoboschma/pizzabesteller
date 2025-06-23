using pizzabesteller.uidUx;

namespace pizzabesteller.Visitor
{
    // The Visitor interface declares a set of visiting methods that
    // correspond to element classes. The signature of a visiting
    // method lets the visitor identify the exact class of the
    // element that it's dealing with.
    public interface IGUIComponentVisitor
    {
        void Visit(DebugTextBox textBox);

        void Visit(DebugButton button);
    }
}