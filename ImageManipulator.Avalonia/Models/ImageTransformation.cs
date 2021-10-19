/* Image Manipulator - (C) 2021 Premysl Fara  */

namespace ImageManipulator.Avalonia.Models
{
    public class ImageTransformation
    {
        #region crop

        /// <summary>
        /// If true, the crop transformation will be used.
        /// </summary>
        public bool ApplyCrop { get; set; }
        
        /// <summary>
        /// The X value of the desired output aspect ratio (int, 0 &lt; X &lt;= Int32.Max).
        /// </summary>
        public int AspectRatioX { get; set; }
        
        /// <summary>
        /// The Y value of the desired output aspect ratio (int, 0 &lt; Y &lt;= Int32.Max).
        /// </summary>
        public int AspectRatioY { get; set; }
        
        #endregion


        #region resize

        /// <summary>
        /// If true, the resize transformation will be used.
        /// </summary>
        public bool ApplyResize { get; set; }
        
        /// <summary>
        /// The maximal image side size in pixels (int, 0 &lt; S &lt;= Int32.Max).
        /// </summary>
        public int MaxImageSideSize { get; set; }

        #endregion


        #region output - JPEG

        /// <summary>
        /// If true, the JPEG output file format will be generated.
        /// </summary>
        public bool GenerateJpeg { get; set; }
        
        /// <summary>
        /// The maximal requested JPEG image quality (0 &lt; Q &lt;= 100).
        /// </summary>
        public int MaxJpegImageQuality { get; set; }
        
        /// <summary>
        /// The maximal JPEG image file size in Bytes.
        /// </summary>
        public int MaxJpegImageSizeBytes { get; set; }
        
        #endregion
        
        
        #region output - PNG

        /// <summary>
        /// If true, the PNG output file format will be generated.
        /// </summary>
        public bool GeneratePng { get; set; }
        
        /// <summary>
        /// The requested PNG compression level (0 &lt;= C &lt; 10).
        /// </summary>
        public int PngCompressionLevel { get; set; }

        #endregion
    }
}