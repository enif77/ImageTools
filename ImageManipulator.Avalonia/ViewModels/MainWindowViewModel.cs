using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ImageManipulator.Avalonia.Models;

namespace ImageManipulator.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ImageInfo> Images { get; set; }

        public string Greeting => "Welcome to Avalonia!";


        public MainWindowViewModel()
        {
            Images = new ObservableCollection<ImageInfo>();

            for (var i = 1; i <= 100; i++)
            {
                Images.Add(new ImageInfo() { DisplayName = "image" + i + ".jpeg", Path = "images/image" + i + ".jpeg" });
            }
        }
    }
}