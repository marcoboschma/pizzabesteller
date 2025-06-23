using System.Windows;
using System.Windows.Controls;

namespace pizzabesteller.Factoryd
{
    public static class ButtonFactory
    {
        public static Button CreateButton(string text, double width, double height, double margin, HorizontalAlignment alignment)
        {
            return new Button
            {
                Content = text,
                Width = width,
                Height = height,
                Margin = new Thickness(margin),
                HorizontalAlignment = alignment
            };
        }
    }
}