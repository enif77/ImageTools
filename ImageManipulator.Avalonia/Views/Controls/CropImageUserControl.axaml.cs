using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ImageManipulator.Avalonia.Views.Controls
{
    public partial class CropImageUserControl : UserControl
    {
        public CropImageUserControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}