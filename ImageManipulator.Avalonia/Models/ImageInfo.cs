/* Image Manipulator - (C) 2021 Premysl Fara  */

namespace ImageManipulator.Avalonia.Models
{
    using System;
    
    
    /// <summary>
    /// Holds information about an image.
    /// </summary>
    public class ImageInfo
    {
        /// <summary>
        /// A path to the image on the disk.
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// A name displayed in the list of images.
        /// </summary>
        public string DisplayName { get; set; }

        
        public ImageInfo(string path, string displayName)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
        }
        

        public override string ToString() =>
            DisplayName;
    }
}