using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ReactiveUI;

namespace ImageManipulator.Avalonia.ViewModels
{
    public class ViewModelBase  : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}