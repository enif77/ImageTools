/* (C) 2021 Přemysl Fára */

namespace ImageManipulator
{
    using System;
    using System.IO;
    
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;


    static class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("Image Manipulator v1.0.0 (2021-09-12)");

            //TestImageMutations();
            
            TestImageCropping(
                "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
                "/Users/enif/Pictures/IMG_20201018_110921-01_16x9.jpeg", 
                16, 9);
            
            TestImageCropping(
                "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
                "/Users/enif/Pictures/IMG_20201018_110921-01_21x9.jpeg", 
                21, 9);
            
            TestImageCropping(
                "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
                "/Users/enif/Pictures/IMG_20201018_110921-01_3x2.jpeg", 
                3, 2);
            
            TestImageCropping(
                "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
                "/Users/enif/Pictures/IMG_20201018_110921-01_4x3.jpeg", 
                4, 3);
            
            TestImageCropping(
                "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
                "/Users/enif/Pictures/IMG_20201018_110921-01_9x16.jpeg", 
                9, 16);
            
            TestImageCropping(
                "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
                "/Users/enif/Pictures/IMG_20201018_110921-01_1x1.jpeg", 
                1, 1);

            TestImageResize(
                "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
                "/Users/enif/Pictures/IMG_20201018_110921-01_1024.jpeg",
                1024);
            
            TestImageResize(
                "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
                "/Users/enif/Pictures/IMG_20201018_110921-01_256.jpeg",
                256);

            TestImageJpegCompression(
                "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
                "/Users/enif/Pictures/IMG_20201018_110921-01_max.jpeg",
                1024 * 25);  // 25 KB
            
            return 0;
        }

        
        private static void TestImageMutations()
        {
            // Open the file automatically detecting the file type to decode it.
            // Our image is now in an uncompressed, file format agnostic, structure in-memory as
            // a series of pixels.
            // You can also specify the pixel format using a type parameter (e.g. Image<Rgba32> image = Image.Load<Rgba32>("foo.jpg"))
            using (var image = Image.Load<Rgba32>("/Users/enif/Pictures/IMG_20201018_110921-01.jpeg"))
            {
                // Resize the image in place and return it for chaining.
                // 'x' signifies the current image processing context.
                image.Mutate(x => x
                    .Resize(image.Width / 2, image.Height / 2)
                    .Crop(new Rectangle(100, 100, 800, 600))
                    .Flip(FlipMode.Horizontal)
                    .Grayscale());

                // The library automatically picks an encoder based on the file extension then
                // encodes and write the data to disk.
                // You can optionally set the encoder to choose.
                image.Save("/Users/enif/Pictures/IMG_20201018_110921-01_s.jpeg");
            }
        }

        
        private static void TestImageCropping(string srcImgPath, string destImagePath, double aspectRatioX, double aspectRatioY)
        {
            using (var image = Image.Load<Rgba32>(srcImgPath))
            {
                image.Crop(aspectRatioX, aspectRatioY);
                image.Save(destImagePath);
            }
        }
        
        
        private static void TestImageResize(string srcImgPath, string destImagePath, int maxImageSideSize)
        {
            using (var image = Image.Load<Rgba32>(srcImgPath))
            {
                image.Resize(maxImageSideSize);
                image.Save(destImagePath);
            }
        }
        
        private static void TestImageJpegCompression(string srcImgPath, string destImagePath, int maxImageJpegSizeBytes)
        {
            using (var image = Image.Load<Rgba32>(srcImgPath))
            {
                image.Resize(1024);

                var imageSaved = false;
                for (var q = 100; q >= 5; q -= 5)
                {
                    byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        image.SaveAsJpeg(ms, new JpegEncoder() { Quality = q });
                        bytes = ms.ToArray();
                    }

                    if (bytes.Length <= maxImageJpegSizeBytes)
                    {
                        File.WriteAllBytes(destImagePath, bytes);
                        imageSaved = true;
                        
                        break;
                    }
                }

                if (imageSaved == false)
                {
                    image.Save(destImagePath);    
                }
            }
        }
        
        /// <summary>
        /// Generates a cropped and resized image from a source image.
        /// </summary>
        /// <param name="image">An input image.</param>
        /// <param name="maxImageSideSize">The maximal image side size. (s <= 0 -> original side size)</param>
        /// <param name="cropToAspectRatio">If true, the output image is cropped to the aspectRatioX/aspectRatioY aspect ratio.</param>
        /// <param name="aspectRatioX">Aspect ratio X.</param>
        /// <param name="aspectRatioY">Aspect ratio Y.</param>
        private static void ExportImage(
            Image<Rgba32> image,
            int maxImageSideSize,
            bool cropToAspectRatio,
            double aspectRatioX,
            double aspectRatioY)
        {
            if (cropToAspectRatio)
            {
                image.Crop(aspectRatioX, aspectRatioY);
            }

            image.Resize(maxImageSideSize);
            
            // TODO: Add JPEG max file size conversion.
        }
        

        /// <summary>
        /// Crops an image to a specific aspect ratio.
        /// </summary>
        /// <param name="image">An image to crop.</param>
        /// <param name="aspectRatioX">Aspect ratio X.</param>
        /// <param name="aspectRatioY">Aspect ratio Y.</param>
        private static void Crop(this Image<Rgba32> image, double aspectRatioX, double aspectRatioY)
        {
            if (image.Width >= image.Height)
            {
                CropLandscapeImage(image, aspectRatioX, aspectRatioY);
            }
            else
            {
                CropPortraitImage(image, aspectRatioX, aspectRatioY);
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
        
        
        /// <summary>
        /// Resizes an image, so none of its sides is bigger than maxImageSideSize.
        /// </summary>
        /// <param name="image">An image to resize.</param>
        /// <param name="maxImageSideSize">The maximum size in pixel of the longer image size.</param>
        private static void Resize(this Image<Rgba32> image, int maxImageSideSize)
        {
            var srcWidth = image.Width;
            var srcHeight = image.Height;
          
            if (maxImageSideSize <= 0 || (srcWidth < maxImageSideSize && srcHeight < maxImageSideSize))
            {
                // Too small or no side size limit.
                return;
            }
            
            int destWidth, destHeight;
            if (srcWidth == srcHeight)
            {
                // Square.
                destWidth = maxImageSideSize;
                destHeight = destWidth;
            }
            else if (srcWidth > srcHeight)
            {
                // Landscape.
                destWidth = maxImageSideSize;
                destHeight = (int)(srcHeight * (destWidth / (double)srcWidth));
            }
            else
            {
                // Portrait.
                destHeight = maxImageSideSize;
                destWidth = (int)(srcWidth * (destHeight / (double)srcHeight));
            }
            
            image.Mutate(x => x
                .Resize(destWidth, destHeight)
            );
        }
        
    }
}

/*

JpegOutputMaxQuality = 100;   // In %.
JpegOutputMinQuality = 5;     // In %.
JpegOutputQualityStep = 5;    // In %.
 
Generates a JPEG image from the image.png source.

param source = An input image.
param outputImagePath = The output image path.
param fileFormat = The output image file format.
param maxJpegFileSize = The maximal output JPEG image file size. (s <= 0 -> no limit)
param maxJpegQuality = The maximal output JPEG image quality. (q <= 0 -> 100%)
param maxImageSideSize = The maximal image side size. (s <= 0 -> original side size)
param cropToAspectRatio = If true, the output image is cropped to the aspectRatioX/aspectRatioY aspect ratio.
param aspectRatioX = Aspect ratio X.
param aspectRatioY = Aspect ratio Y.

protected void ExportImage(
    Image source,
    string outputImagePath,
    string fileFormat,
    int maxJpegQuality,
    int maxJpegFileSize,
    int maxImageSideSize,
    bool cropToAspectRatio,
    double aspectRatioX,
    double aspectRatioY) 
 
 */