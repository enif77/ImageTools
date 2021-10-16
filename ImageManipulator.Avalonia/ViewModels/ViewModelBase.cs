/* Image Manipulator - (C) 2021 Premysl Fara  */

namespace ImageManipulator.Avalonia.ViewModels
{
    using System.ComponentModel;

    
    public class ViewModelBase  : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}