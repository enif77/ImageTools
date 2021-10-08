using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Avalonia.Controls.Selection;
using ImageManipulator.Avalonia.Models;

namespace ImageManipulator.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ImageInfoViewModel> Images { get; }
        
        public SelectionModel<ImageInfoViewModel> Selection { get; }
        
        public string Greeting => "Welcome to Avalonia!";


        public MainWindowViewModel()
        {
            Images = new ObservableCollection<ImageInfoViewModel>();
            Selection = new SelectionModel<ImageInfoViewModel>
            {
                SingleSelect = false
            };
            Selection.SelectionChanged += SelectionChanged;
        }

        
        void SelectionChanged(object sender, SelectionModelSelectionChangedEventArgs e)
        {
            // ... handle selection changed
        }
        

        public void AddImagesButtonClicked()
        {
            Selection.Clear();
            Images.Clear();
            
            foreach (var image in GetImagesFromPath("/Users/enif/Pictures"))
            {
                Images.Add(new ImageInfoViewModel(image));
            }
        }


        public void RemoveImagesButtonClicked()
        {
            if (Selection.Count == 0)
            {
                return;
            }

            var selectedImages = new List<ImageInfoViewModel>(Selection.SelectedItems);
            Selection.Clear();
            foreach (var selectedImage in selectedImages)
            {
                Images.Remove(selectedImage);
            }
        }


        private IEnumerable<ImageInfo> GetImagesFromPath(string path)
        {
            var images = new List<ImageInfo>();

            if (string.IsNullOrEmpty(path) || Directory.Exists(path) == false)
            {
                return images;
            }
            
            var files = System.IO.Directory.GetFiles(path, "*.jpeg");
            foreach (var file in files)
            {
                images.Add(new ImageInfo() { DisplayName = Path.GetFileName(file), Path = file });
            }

            return images;
        }

    }
}