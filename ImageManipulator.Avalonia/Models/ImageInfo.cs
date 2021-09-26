namespace ImageManipulator.Avalonia.Models
{
    /// <summary>
    /// Holds information about an image.
    /// </summary>
    public class ImageInfo
    {
        /// <summary>
        /// A path to the image on a disk.
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// A name displayed in the list of images.
        /// </summary>
        public string DisplayName { get; set; }
    }
}