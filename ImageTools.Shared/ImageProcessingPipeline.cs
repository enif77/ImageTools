/* (C) 2021 Přemysl Fára */

namespace ImageTools.Shared
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    
    using ImageTools.Shared.Encoders;
    using ImageTools.Shared.Transformations;


    /// <summary>
    /// Pipeline for images processing.
    /// </summary>
    public class ImageProcessingPipeline
    {
        /// <summary>
        /// A list of transformations, that will be applied to an image.
        /// </summary>
        public IList<IImageTransformation> ImageTransformations
            => new List<IImageTransformation>(_imageTransformations);

        /// <summary>
        /// An image encoder to be used for generating the output image.
        /// Its set tu the PngImageEncoder by default.
        /// </summary>
        public IImageEncoder ImageEncoder => _imageEncoder;


        /// <summary>
        /// Constructor.
        /// </summary>
        public ImageProcessingPipeline()
        {
            _imageTransformations = new List<IImageTransformation>();
            
            SetImageEncoder(new PngImageEncoder());
        }


        /// <summary>
        /// Adds an image transformation to the list of image transformations.
        /// </summary>
        /// <param name="imageTransformation">An image transformation.</param>
        /// <exception cref="ArgumentNullException">Thrown, when the imageTransformation parameter is null.</exception>
        public void AddImageTransformation(IImageTransformation imageTransformation)
        {
            if (imageTransformation == null) throw new ArgumentNullException(nameof(imageTransformation));
            
            _imageTransformations.Add(imageTransformation);
        }

        /// <summary>
        /// Sets an image encoder.
        /// </summary>
        /// <param name="imageEncoder">An image encoder.</param>
        /// <exception cref="ArgumentNullException">Thrown, when the imageEncoder parameter is null.</exception>
        public void SetImageEncoder(IImageEncoder imageEncoder)
        {
            _imageEncoder = imageEncoder ?? throw new ArgumentNullException(nameof(imageEncoder));
        }

        /// <summary>
        /// Applies all transformations to an image and encodes it using the predefined image encoder.
        /// NOTE: Modifies the original image! 
        /// </summary>
        /// <param name="image">An input image.</param>
        /// <returns>Transformed and encoded image.</returns>
        /// <exception cref="ArgumentNullException">Thrown, when the image parameter is null.</exception>
        public byte[] Execute(Image<Rgba32> image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            
            foreach (var transformation in _imageTransformations)
            {
                transformation.Execute(image);
            }
            
            byte[] transformedImageBytes;
            using (var ms = new MemoryStream())
            {
                _imageEncoder.Encode(image, ms);
                transformedImageBytes = ms.ToArray();
            }
            
            return transformedImageBytes;
        }
        
        
        private readonly IList<IImageTransformation> _imageTransformations;
        private IImageEncoder _imageEncoder;
    }
}