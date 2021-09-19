/* (C) 2021 Přemysl Fára */

namespace ImageTools.Shared.Transformations
{
    using System;
    
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    
    /// <summary>
    /// Crops an image to a defined aspect ratio.
    /// </summary>
    public class CropImageTransformation : IImageTransformation
    {
        public string Name => "Crop";
        
        /// <summary>
        /// The X value of the desired output aspect ratio (double, 0 &lt; X &lt;= Double.Max).
        /// </summary>
        public double AspectRatioX { get; }
        
        /// <summary>
        /// The Y value of the desired output aspect ratio (double, 0 &lt; Y &lt;= Double.Max).
        /// </summary>
        public double AspectRatioY { get; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="aspectRatioX">A X value of the desired output aspect ratio.</param>
        /// <param name="aspectRatioY">A Y value of the desired output aspect ratio.</param>
        public CropImageTransformation(double aspectRatioX, double aspectRatioY)
        {
            if (aspectRatioX <= 0) throw new ArgumentOutOfRangeException(nameof(aspectRatioX), aspectRatioX, "A value greater than zero expected.");
            if (aspectRatioY <= 0) throw new ArgumentOutOfRangeException(nameof(aspectRatioY), aspectRatioY, "A value greater than zero expected.");

            AspectRatioX = aspectRatioX;
            AspectRatioY = aspectRatioY;
        }


        public void Execute(Image<Rgba32> image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            
            if (image.Width >= image.Height)
            {
                CropLandscapeImage(image, AspectRatioX, AspectRatioY);
            }
            else
            {
                CropPortraitImage(image, AspectRatioX, AspectRatioY);
            }
        }

        
        private static void CropLandscapeImage(Image<Rgba32> image, double aspectRatioX, double aspectRatioY)
        {
            var imgWidth = (double)image.Width;
            var imgHeight = (double)image.Height;

            // Images more wide, than the aspect ratio requires.
            if (imgWidth / imgHeight > (aspectRatioX / aspectRatioY))
            {
                var extraWidth = imgWidth - (imgHeight * (aspectRatioX / aspectRatioY));
                var cropStartFrom = extraWidth / 2.0;
                var destWidth = (int)(imgWidth - extraWidth);
                
                image.Mutate(x => x
                    .Crop(new Rectangle((int)cropStartFrom, 0, destWidth, image.Height))
                );
            }
            else
            {
                // Images more (or as narrow as the aspect ratio) narrow, than the aspect ration requires. 
                var extraHeight = imgHeight - (imgWidth * (aspectRatioY / aspectRatioX));
                var cropStartFrom = extraHeight / 2.0;
                var destHeight = (int)(imgHeight - extraHeight);
                
                image.Mutate(x => x
                    .Crop(new Rectangle(0, (int)cropStartFrom, image.Width, destHeight))
                );
            }
        }
        
        
        private static void CropPortraitImage(Image<Rgba32> image, double aspectRatioX, double aspectRatioY)
        {
            var imgWidth = (double)image.Width;
            var imgHeight = (double)image.Height;

            // Images more tall, than the aspect ratio requires.
            if (imgHeight / imgWidth > (aspectRatioX / aspectRatioY))
            {
                var extraHeight = imgHeight - (imgWidth * (aspectRatioX / aspectRatioY));
                var cropStartFrom = extraHeight / 2.0;
                var destHeight = (int)(imgHeight - extraHeight);
                
                image.Mutate(x => x
                    .Crop(new Rectangle(0, (int)cropStartFrom, image.Width, destHeight))
                );
            }
            else
            {
                // Images more (or as narrow as the aspect ratio) narrow, than the aspect ration requires. 
                var extraWidth = imgWidth - (imgHeight * (aspectRatioY / aspectRatioX));
                var cropStartFrom = extraWidth / 2.0;
                var destWidth = (int)(imgWidth - extraWidth);
               
                image.Mutate(x => x
                    .Crop(new Rectangle((int)cropStartFrom, 0, destWidth, image.Height))
                );
            }
        }
    }
}