/* (C) 2021 Přemysl Fára */

namespace ImageTools.Shared.Transformations
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    
    /// <summary>
    /// Describes an image transformation.
    /// </summary>
    public interface IImageTransformation
    {
        /// <summary>
        /// A human readable transformation name. 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Executes an image transformation.
        /// </summary>
        /// <param name="image">An image, this transformation should be applied to.</param>
        public void Execute(Image<Rgba32> image);
    }
}