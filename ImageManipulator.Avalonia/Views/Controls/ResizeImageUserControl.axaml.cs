using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ImageManipulator.Avalonia.Views.Controls
{
    public class ResizeImageUserControl : UserControl
    {
        public ResizeImageUserControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}