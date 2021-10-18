/* Image Manipulator - (C) 2021 Premysl Fara  */

using Avalonia.Controls;
using Avalonia.Controls.Selection;

namespace ImageManipulator.Avalonia.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    
    using ImageManipulator.Avalonia.Models;
    using ImageManipulator.Avalonia.Services;

    
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IAppService _appService;
        public Window? MainWindow { get; set; }
        
        
        #region files selector
        
        /// <summary>
        /// The list of images, that should be processed.
        /// </summary>
        public ObservableCollection<ImageInfoViewModel> Images { get; }
        
        /// <summary>
        /// The list of selected images.
        /// </summary>
        public SelectionModel<ImageInfoViewModel> Selection { get; }
        
        #endregion


        #region main

        private ImageTransformation Model { get; }
        
        
        public string Greeting => "Welcome to Images Manipulator!";

        // Crop

        /// <summary>
        /// If true, the crop transformation will be used.
        /// </summary>
        public bool ApplyCrop
        {
            get => Model.ApplyCrop;

            set
            {
                if (value == Model.ApplyCrop)
                {
                    return;
                }

                Model.ApplyCrop = value;
                OnPropertyChanged(nameof(ApplyCrop));
            }
        }


        /// <summary>
        /// The list of available image aspect ratio crop presets.
        /// </summary>
        public ObservableCollection<ImageCropPreset> ImageCropPresets { get; }

        private ImageCropPreset _selectedImageCropPreset = null!;

        /// <summary>
        /// The currently selected image aspect ratio crop preset.
        /// </summary>
        public ImageCropPreset SelectedImageCropPreset
        {
            get => _selectedImageCropPreset;

            set
            {
                if (value == _selectedImageCropPreset)
                {
                    return;
                }

                _selectedImageCropPreset = value;
                OnPropertyChanged(nameof(SelectedImageCropPreset));
                
                AspectRatioX = _selectedImageCropPreset.AspectRatioX;
                AspectRatioY = _selectedImageCropPreset.AspectRatioY;
            }
        }


        /// <summary>
        /// The X value of the desired output aspect ratio (int, 0 &lt; X &lt;= Int32.Max).
        /// </summary>
        public int AspectRatioX
        {
            get => Model.AspectRatioX;

            set
            {
                if (value == Model.AspectRatioX)
                {
                    return;
                }

                Model.AspectRatioX = value;
                OnPropertyChanged(nameof(AspectRatioX));
            }
        }
        
        /// <summary>
        /// The Y value of the desired output aspect ratio (int, 0 &lt; Y &lt;= Int32.Max).
        /// </summary>
        public int AspectRatioY
        {
            get => Model.AspectRatioY;

            set
            {
                if (value == Model.AspectRatioY)
                {
                    return;
                }

                Model.AspectRatioY = value;
                OnPropertyChanged(nameof(AspectRatioY));
            }
        }
        
        // Resize
        
        /// <summary>
        /// If true, the resize transformation will be used.
        /// </summary>
        public bool ApplyResize
        {
            get => Model.ApplyResize;

            set
            {
                if (value == Model.ApplyResize)
                {
                    return;
                }

                Model.ApplyResize = value;
                OnPropertyChanged(nameof(ApplyResize));
            }
        }
        
        /// <summary>
        /// The maximal image side size in pixels (int, 0 &lt; S &lt;= Int32.Max).
        /// </summary>
        public int MaxImageSideSize
        {
            get => Model.MaxImageSideSize;

            set
            {
                if (value == Model.MaxImageSideSize)
                {
                    return;
                }

                Model.MaxImageSideSize = value;
                OnPropertyChanged(nameof(MaxImageSideSize));
            }
        }
        
        // Output
        
        /// <summary>
        /// If true, the JPEG output file format will be used.
        /// </summary>
        public bool UseJpegOutputFileFormat
        {
            get => Model.UseJpegOutputFileFormat;

            set
            {
                if (value == Model.UseJpegOutputFileFormat)
                {
                    return;
                }

                Model.UseJpegOutputFileFormat = value;
                OnPropertyChanged(nameof(UseJpegOutputFileFormat));
            }
        }
        
        /// <summary>
        /// The maximal requested JPEG image quality (0 &lt; Q &lt;= 100).
        /// </summary>
        public int MaxJpegImageQuality
        {
            get => Model.MaxJpegImageQuality;

            set
            {
                if (value == Model.MaxJpegImageQuality)
                {
                    return;
                }

                Model.MaxJpegImageQuality = value;
                OnPropertyChanged(nameof(MaxJpegImageQuality));
            }
        }
        
        /// <summary>
        /// The maximal JPEG image file size in Bytes.
        /// </summary>
        public int MaxJpegImageSizeBytes
        {
            get => Model.MaxJpegImageSizeBytes;

            set
            {
                if (value == Model.MaxJpegImageSizeBytes)
                {
                    return;
                }

                Model.MaxJpegImageSizeBytes = value;
                OnPropertyChanged(nameof(MaxJpegImageSizeBytes));
            }
        }
        
        /// <summary>
        /// The requested PNG compression level (0 &lt;= C &lt; 10).
        /// </summary>
        public int PngCompressionLevel
        {
            get => Model.PngCompressionLevel;

            set
            {
                if (value == Model.PngCompressionLevel)
                {
                    return;
                }

                Model.PngCompressionLevel = value;
                OnPropertyChanged(nameof(PngCompressionLevel));
            }
        }
        
        #endregion


        public MainWindowViewModel()
            : this(
                new AppService(),
                new ImageTransformation())
        {
        }
        

        public MainWindowViewModel(
            IAppService appService,
            ImageTransformation model)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            
            Images = new ObservableCollection<ImageInfoViewModel>();
            Selection = new SelectionModel<ImageInfoViewModel>
            {
                SingleSelect = false
            };
            //Selection.SelectionChanged += SelectionChanged;

            ImageCropPresets = new ObservableCollection<ImageCropPreset>(_appService.GetImageCropPresets());
            SelectedImageCropPreset = ImageCropPresets[0];
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

            var selectedFiles = await ofd.ShowAsync(MainWindow);
            if (selectedFiles == null)
            {
                return;
            }

            foreach (var selectedFilePath in selectedFiles)
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
                    new ImageInfo(
                        selectedFilePath,
                        Path.GetFileName(selectedFilePath))),
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