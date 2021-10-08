using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using ImageManipulator.Avalonia.Models;

namespace ImageManipulator.Avalonia.ViewModels
{
    public class ImageInfoViewModel : INotifyPropertyChanged
    {
        public ImageInfo Model { get; }

        public string DisplayName
        {
            get => Model.DisplayName;

            set
            {
                if (value == Model.DisplayName)
                {
                    return;
                }
                
                Model.DisplayName = value;
                    
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayName)));
            }
        }

        public string Path
        {
            get => Model.Path;

            set
            {
                if (value == Model.Path)
                {
                    return;
                }
                
                Model.Path = value;
                    
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
            }
        }
        
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        
        public ImageInfoViewModel(ImageInfo model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        
        public override string ToString()
        {
            return DisplayName;
        }
    }
}