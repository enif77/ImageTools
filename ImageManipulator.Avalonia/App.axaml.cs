using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ImageManipulator.Avalonia.Models;
using ImageManipulator.Avalonia.ViewModels;
using ImageManipulator.Avalonia.Views;

namespace ImageManipulator.Avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                desktop.MainWindow.DataContext = new MainWindowViewModel(
                    new ImageTransformation())
                {
                    MainWindow = desktop.MainWindow
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}