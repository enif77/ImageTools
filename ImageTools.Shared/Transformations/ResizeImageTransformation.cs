/* (C) 2021 Přemysl Fára */

namespace ImageTools.Shared.Transformations
{
    using System;
    
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    
    /// <summary>
    /// Resizes an image to a defined maximal image side size in pixels.
    /// </summary>
    public class ResizeImageTransformation : IImageTransformation
    {
        public string Name => "Resize";
        
        /// <summary>
        /// The maximal image side size in pixels (int, 0 &lt; S &lt;= Int32.Max).
        /// </summary>
        public int MaxImageSideSize { get; }

        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxImageSideSize">A maximal image side size in pixels.</param>
        public ResizeImageTransformation(int maxImageSideSize)
        {
            if (maxImageSideSize <= 0) throw new ArgumentOutOfRangeException(nameof(maxImageSideSize), maxImageSideSize, "A value greater than zero expected.");

            MaxImageSideSize = maxImageSideSize;
        }
        
        
        public void Execute(Image<Rgba32> image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            
            var srcWidth = image.Width;
            var srcHeight = image.Height;
          
            if (MaxImageSideSize <= 0 || (srcWidth < MaxImageSideSize && srcHeight < MaxImageSideSize))
            {
                // Too small or no side size limit.
                return;
            }
            
            int destWidth, destHeight;
            if (srcWidth == srcHeight)
            {
                // Square.
                destWidth = MaxImageSideSize;
                destHeight = destWidth;
            }
            else if (srcWidth > srcHeight)
            {
                // Landscape.
                destWidth = MaxImageSideSize;
                destHeight = (int)(srcHeight * (destWidth / (double)srcWidth));
            }
            else
            {
                // Portrait.
                destHeight = MaxImageSideSize;
                destWidth = (int)(srcWidth * (destHeight / (double)srcHeight));
            }
            
            image.Mutate(x => x
                .Resize(destWidth, destHeight)
            );
        }
    }
}