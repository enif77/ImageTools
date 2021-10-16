using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ImageManipulator.Avalonia.Views.Controls
{
    public class OutputJpegUserControl : UserControl
    {
        public OutputJpegUserControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}