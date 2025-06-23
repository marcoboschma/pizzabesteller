using System.Windows.Controls;

namespace pizzabesteller.uidUx
{
    public interface IServerPanelTemp
    {
        Button ConnectToServer
        {
            get;
        }

        TextBox Description
        {
            get;
        }

        TextBox Messagelog
        {
            get;
            set;
        }

        TextBox Title
        {
            get;
        }

        StackPanel RenderPanel();
    }
}