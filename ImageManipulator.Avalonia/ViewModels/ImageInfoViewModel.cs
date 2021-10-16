/* Image Manipulator - (C) 2021 Premysl Fara  */

namespace ImageManipulator.Avalonia.ViewModels
{
    using System;
    using System.ComponentModel;

    using ImageManipulator.Avalonia.Models;

    
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