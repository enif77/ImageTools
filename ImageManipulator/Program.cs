/* (C) 2021 Přemysl Fára */

using ImageTools.Core;
using ImageTools.Shared;
using ImageTools.Shared.Encoders;

namespace ImageManipulator
{
    using System;
    using System.IO;
    
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    using ImageTools.Shared.Transformations;

    
    static class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("Image Manipulator v1.0.0");
            
            var imagePath = "/Users/enif/Devel/Temp/PanoView/letiste-pano.bmp";
            
            using (var image = Image.Load<Rgba32>(imagePath))
            {
                var imageData = ImageRgbaToImageData(image);

                RenderCubeFace(imageData, CubeMapFaceOrientation.NegativeX, "/Users/enif/Devel/Temp/PanoView/letiste-pano_nx.bmp");
                RenderCubeFace(imageData, CubeMapFaceOrientation.PositiveX, "/Users/enif/Devel/Temp/PanoView/letiste-pano_px.bmp");
                RenderCubeFace(imageData, CubeMapFaceOrientation.NegativeY, "/Users/enif/Devel/Temp/PanoView/letiste-pano_ny.bmp");
                RenderCubeFace(imageData, CubeMapFaceOrientation.PositiveY, "/Users/enif/Devel/Temp/PanoView/letiste-pano_py.bmp");
                RenderCubeFace(imageData, CubeMapFaceOrientation.NegativeZ, "/Users/enif/Devel/Temp/PanoView/letiste-pano_nz.bmp");
                RenderCubeFace(imageData, CubeMapFaceOrientation.PositiveZ, "/Users/enif/Devel/Temp/PanoView/letiste-pano_pz.bmp");
            }
            
            //TestImageMutations();
            
            // TestImageCropping(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_16x9.jpeg", 
            //     16, 9);
            //
            // TestImageCropping(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_21x9.jpeg", 
            //     21, 9);
            //
            // TestImageCropping(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_3x2.jpeg", 
            //     3, 2);
            //
            // TestImageCropping(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_4x3.jpeg", 
            //     4, 3);
            //
            // TestImageCropping(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_9x16.jpeg", 
            //     9, 16);
            //
            // TestImageCropping(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_1x1.jpeg", 
            //     1, 1);
            //
            // TestImageResize(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_1024.jpeg",
            //     1024);
            //
            // TestImageResize(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_256.jpeg",
            //     256);
            //
            // TestImageJpegCompression(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_max.jpeg",
            //     1024 * 25);  // 25 KB
            //
            // TestImagePngCompression(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.png",
            //     6);
            //
            // TestImagePngCompression(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_max.png",
            //     9);
            //
            // TestImagePipeline(
            //     "/Users/enif/Pictures/IMG_20201018_110921-01.jpeg",
            //     "/Users/enif/Pictures/IMG_20201018_110921-01_pipeline.jpeg");
            
            return 0;
        }

        private static void RenderCubeFace(ImageData imageData, CubeMapFaceOrientation faceOrientation, string path)
        {
            var convertor = new EquirectangularProjectionToCubeMapConverter();
                
            var face = convertor.RenderFace(
                imageData,
                faceOrientation,
                0,
                ImageInterpolationStrategy.NearestNeighbor,
                1024,
                1);
                
            var destImage = ImageDataToImageRgba(face);

            destImage.Save(path);
        }


        private static ImageData ImageRgbaToImageData(Image<Rgba32> image)
        {
            var imageData = new ImageData(image.Width, image.Height);
            
            var i = 0;
            for (var y = 0; y < image.Height; y++)
            {
                for (var x = 0; x < image.Width; x++)
                {
                    var pixel = image[x, y];
                        
                    imageData.Data[i++] = pixel.R;
                    imageData.Data[i++] = pixel.G;
                    imageData.Data[i++] = pixel.B;
                    imageData.Data[i++] = pixel.A;
                }
            }

            return imageData;
        }
        
        
        private static Image<Rgba32> ImageDataToImageRgba(ImageData face)
        {
            var destImage = new Image<Rgba32>(face.Width, face.Height);
            for (var y = 0; y < face.Height; y++)
            {
                for (var x = 0; x < face.Width; x++)
                {
                    var idx = (y * face.Width + x) * 4;
                        
                    var r = face.Data[idx + 0];
                    var g = face.Data[idx + 1];
                    var b = face.Data[idx + 2];
                    var a = face.Data[idx + 3];
                        
                    destImage[x, y] = new Rgba32(r, g, b, a);
                }
            }

            return destImage;
        }


        // private static void TestImageMutations()
        // {
        //     // Open the file automatically detecting the file type to decode it.
        //     // Our image is now in an uncompressed, file format agnostic, structure in-memory as
        //     // a series of pixels.
        //     // You can also specify the pixel format using a type parameter (e.g. Image<Rgba32> image = Image.Load<Rgba32>("foo.jpg"))
        //     using (var image = Image.Load<Rgba32>("/Users/enif/Pictures/IMG_20201018_110921-01.jpeg"))
        //     {
        //         // Resize the image in place and return it for chaining.
        //         // 'x' signifies the current image processing context.
        //         image.Mutate(x => x
        //             .Resize(image.Width / 2, image.Height / 2)
        //             .Crop(new Rectangle(100, 100, 800, 600))
        //             .Flip(FlipMode.Horizontal)
        //             .Grayscale());
        //
        //         // The library automatically picks an encoder based on the file extension then
        //         // encodes and write the data to disk.
        //         // You can optionally set the encoder to choose.
        //         image.Save("/Users/enif/Pictures/IMG_20201018_110921-01_s.jpeg");
        //     }
        // }

        
        private static void TestImageCropping(string srcImgPath, string destImagePath, double aspectRatioX, double aspectRatioY)
        {
            using (var image = Image.Load<Rgba32>(srcImgPath))
            {
                new CropImageTransformation(aspectRatioX, aspectRatioY).Execute(image);
                image.Save(destImagePath);
            }
        }
        
        
        private static void TestImageResize(string srcImgPath, string destImagePath, int maxImageSideSize)
        {
            using (var image = Image.Load<Rgba32>(srcImgPath))
            {
                new ResizeImageTransformation(maxImageSideSize).Execute(image);
                image.Save(destImagePath);
            }
        }
        
        
        private static void TestImageJpegCompression(string srcImgPath, string destImagePath, int maxImageJpegSizeBytes)
        {
            using (var image = Image.Load<Rgba32>(srcImgPath))
            {
                new ResizeImageTransformation(1024).Execute(image);

                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    new JpegImageEncoder(100, maxImageJpegSizeBytes).Encode(image, ms);
                    bytes = ms.ToArray();
                }
                
                File.WriteAllBytes(destImagePath, bytes);
            }
        }
        
        
        private static void TestImagePngCompression(string srcImgPath, string destImagePath, int compressionLevel)
        {
            using (var image = Image.Load<Rgba32>(srcImgPath))
            {
                new ResizeImageTransformation(1024).Execute(image);

                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    new PngImageEncoder(compressionLevel).Encode(image, ms);
                    bytes = ms.ToArray();
                }
                
                File.WriteAllBytes(destImagePath, bytes);
            }
        }
        
        
        private static void TestImagePipeline(string srcImgPath, string destImagePath)
        {
            using (var image = Image.Load<Rgba32>(srcImgPath))
            {
                var pipeline = new ImageProcessingPipeline();
                
                pipeline.AddImageTransformation(new CropImageTransformation(1, 1));
                pipeline.AddImageTransformation(new ResizeImageTransformation(1024));
                
                pipeline.SetImageEncoder(new JpegImageEncoder());
                
                File.WriteAllBytes(destImagePath, pipeline.Execute(image));
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
                new CropImageTransformation(aspectRatioX, aspectRatioY).Execute(image);
            }

            new ResizeImageTransformation(maxImageSideSize).Execute(image);
            
            // TODO: Add JPEG max file size conversion.
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