/* (C) 2021 Přemysl Fára */

namespace ImageTools.Shared.Encoders
{
    using System.IO;

    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    
    /// <summary>
    /// Represents an image encoder.
    /// </summary>
    public interface IImageEncoder
    {
        /// <summary>
        /// Encodes an image to a desired output format.
        /// </summary>
        /// <param name="image">An input image.</param>
        /// <param name="outputStream">An output stream.</param>
        void Encode(Image<Rgba32> image, Stream outputStream);
    }
}