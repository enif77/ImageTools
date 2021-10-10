using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using ImageManipulator.Avalonia.Models;

namespace ImageManipulator.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public Window? MainWindow { get; set; }
        public ObservableCollection<ImageInfoViewModel> Images { get; }
        
        public SelectionModel<ImageInfoViewModel> Selection { get; }
        
        public string Greeting => "Welcome to Images Manipulator!";


        public MainWindowViewModel()
        {
            Images = new ObservableCollection<ImageInfoViewModel>();
            Selection = new SelectionModel<ImageInfoViewModel>
            {
                SingleSelect = false
            };
            //Selection.SelectionChanged += SelectionChanged;
        }

        
        // void SelectionChanged(object sender, SelectionModelSelectionChangedEventArgs e)
        // {
        //     // ... handle selection changed
        // }


        public void AboutMenuItemClicked()
        {
            
        }


        public async void AddImagesButtonClickedAsync()
        {
            Selection.Clear();

            // TODO: Move OpenFileDialog to UI.
            var ofd = new OpenFileDialog
            {
                AllowMultiple = true,
                Title = "Choose images to add"
            };

            foreach (var selectedFilePath in await ofd.ShowAsync(MainWindow))
            {
                if (Images.Any(existingImage => existingImage.Path == selectedFilePath))
                {
                    continue;
                }
                
                var selectedFileExtension = Path.GetExtension(selectedFilePath);
                if (string.IsNullOrEmpty(selectedFileExtension))
                {
                    continue;
                }

                selectedFileExtension = selectedFileExtension.ToLowerInvariant();
                if (selectedFileExtension != ".jpeg" &&
                    selectedFileExtension != ".jpg" &&
                    selectedFileExtension != ".png")
                {
                    continue;
                }

                var index = Images.InsertInPlace(new ImageInfoViewModel(
                    new ImageInfo()
                    {
                        DisplayName = Path.GetFileName(selectedFilePath),
                        Path = selectedFilePath
                    }),
                    o => o.DisplayName);
                
                Selection.Select(index);
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


        // private IEnumerable<ImageInfo> GetImagesFromPath(string path)
        // {
        //     var images = new List<ImageInfo>();
        //
        //     if (string.IsNullOrEmpty(path) || Directory.Exists(path) == false)
        //     {
        //         return images;
        //     }
        //     
        //     var files = System.IO.Directory.GetFiles(path, "*.jpeg");
        //     foreach (var file in files)
        //     {
        //         images.Add(new ImageInfo() { DisplayName = Path.GetFileName(file), Path = file });
        //     }
        //
        //     return images;
        // }

    }
}