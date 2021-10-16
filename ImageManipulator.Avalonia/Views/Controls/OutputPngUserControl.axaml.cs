using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ImageManipulator.Avalonia.Views.Controls
{
    public class OutputPngUserControl : UserControl
    {
        public OutputPngUserControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}