/* Image Manipulator - (C) 2021 Premysl Fara  */

namespace ImageManipulator.Avalonia.Models
{
    using System;
    using System.Globalization;

    
    /// <summary>
    /// Represents a requested image aspect ratio preset for the crop operation.
    /// </summary>
    public class ImageCropPreset : ALookup
    {
        /// <summary>
        /// The X value of the desired output aspect ratio (int, 0 &lt; X &lt;= Int32.Max).
        /// </summary>
        public int AspectRatioX { get; }
        
        /// <summary>
        /// The Y value of the desired output aspect ratio (int, 0 &lt; X &lt;= Int32.Max).
        /// </summary>
        public int AspectRatioY { get; }

        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">An unique ID of this preset.</param>
        /// <param name="aspectRatioX">An aspect ratio X.</param>
        /// <param name="aspectRatioY">An aspect ratio Y.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the aspect ratio X or Y is less or equal to zero.</exception>
        public ImageCropPreset(int id, int aspectRatioX, int aspectRatioY)
        {
            if (aspectRatioX <= 0) throw new ArgumentOutOfRangeException(nameof(aspectRatioX), aspectRatioX,"The aspect ratio X must be a positive number.");
            if (aspectRatioY <= 0) throw new ArgumentOutOfRangeException(nameof(aspectRatioY), aspectRatioY,"The aspect ratio Y must be a positive number.");

            Id = id; 
            Name = $"{ aspectRatioX.ToString(CultureInfo.InvariantCulture) }:{ aspectRatioY.ToString(CultureInfo.InvariantCulture) }";
            AspectRatioX = aspectRatioX;
            AspectRatioY = aspectRatioY;
        }
    }
}