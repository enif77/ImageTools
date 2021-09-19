/* (C) 2021 Přemysl Fára */

namespace ImageTools.Shared.Encoders
{
    using System;
    using System.IO;
    
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.PixelFormats;
    
    
    /// <summary>
    /// Encodes an image to JPEG.
    /// </summary>
    public class JpegImageEncoder : IImageEncoder
    {
        /// <summary>
        /// The maximal requested JPEG image quality (0 &lt; Q &lt;= 100).
        /// </summary>
        public int MaxJpegImageQuality { get; }
        
        /// <summary>
        /// The maximal JPEG image file size in Bytes.
        /// </summary>
        public int MaxJpegImageSizeBytes { get; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxJpegImageQuality">The maximal JPEG image quality (0 &lt; Q &lt;= 100). Its 95 by default.</param>
        /// <param name="maxJpegImageSizeBytes">A maximal output file size in bytes. Its 0 (no limit) by default.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when the maxJpegImageQuality is out of the 0 &lt; Q &lt;= 100 range.</exception>
        public JpegImageEncoder(int maxJpegImageQuality = 95, int maxJpegImageSizeBytes = 0)
        {
            if (maxJpegImageQuality < 1 || maxJpegImageQuality > 100) throw new ArgumentOutOfRangeException(nameof(maxJpegImageQuality), maxJpegImageQuality, "Expected a value from the 0 < N <= 100 range.");

            MaxJpegImageQuality = maxJpegImageQuality;
            MaxJpegImageSizeBytes = maxJpegImageSizeBytes;
        }


        public void Encode(Image<Rgba32> image, Stream outputStream)
        {
            if (MaxJpegImageSizeBytes <= 0)
            {
                // No output size limit.
                image.SaveAsJpeg(outputStream, new JpegEncoder()
                {
                    Quality = MaxJpegImageQuality
                });
                
                return;
            }

            byte[] bytes = null;
            for (var q = MaxJpegImageQuality; q > 0; q -= 5)  // TODO: Use some kind of interval splitting to find a usable quality. 
            {
                using (var ms = new MemoryStream())
                {
                    image.SaveAsJpeg(ms, new JpegEncoder() { Quality = q });
                    bytes = ms.ToArray();
                }

                if (bytes.Length <= MaxJpegImageSizeBytes)
                {
                    break;
                }
            }
           
            // Nothing encoded yet?  
            if (bytes == null)
            {
                // Should never happen...
                image.SaveAsJpeg(outputStream, new JpegEncoder()
                {
                    Quality = MaxJpegImageQuality
                });
            }
            else
            {
                // We have a JPEG!
                outputStream.Write(bytes, 0, bytes.Length - 1);
            }
        }
    }
}