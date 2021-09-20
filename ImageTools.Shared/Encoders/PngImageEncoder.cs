/* (C) 2021 Přemysl Fára */

namespace ImageTools.Shared.Encoders
{
    using System;
    using System.IO;
    
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Png;
    using SixLabors.ImageSharp.PixelFormats;
    
    
    /// <summary>
    /// Encodes an image to PNG.
    /// </summary>
    public class PngImageEncoder : IImageEncoder
    {
        /// <summary>
        /// The requested PNG compression level (0 &lt;= C &lt; 10).
        /// </summary>
        public int CompressionLevel { get; }

        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="compressionLevel">The requested PNG compression level (0 &lt;= C &lt; 10). Its 6 by default.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when the compressionLevel is out of the 0 &lt;= C &lt; 10 range.</exception>
        public PngImageEncoder(int compressionLevel = 6)
        {
            if (compressionLevel < 0 || compressionLevel > 9) throw new ArgumentOutOfRangeException(nameof(compressionLevel), compressionLevel, "Expected a value from the 0 <= N < 10 range.");

            CompressionLevel = compressionLevel;
        }


        public void Encode(Image<Rgba32> image, Stream outputStream)
        {
            image.SaveAsPng(outputStream, new PngEncoder
            {
                CompressionLevel = (PngCompressionLevel)CompressionLevel
            });
        }
    }
}