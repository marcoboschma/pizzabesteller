using System.Windows;
using System.Windows.Controls;

namespace pizzabesteller.Factoryd
{
    public static class TextBoxFactory
    {
        public static TextBox CreateTextBox(string text, double width, double height, double margin, HorizontalAlignment horizontalAlignment)
        {
            return new TextBox
            {
                Text = text,
                Width = width,
                Height = height,
                Margin = new Thickness(margin),
                HorizontalAlignment = horizontalAlignment
            };
        }

        public static TextBox CreateTextBox(string text, double width, double height, double margin, HorizontalAlignment horizontalAlignment, TextWrapping wrapping)
        {
            return new TextBox
            {
                Text = text,
                Width = width,
                Height = height,
                Margin = new Thickness(margin),
                HorizontalAlignment = horizontalAlignment,
                TextWrapping = wrapping
            };
        }
    }
}