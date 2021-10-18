/* Image Manipulator - (C) 2021 Premysl Fara  */

namespace ImageManipulator.Avalonia
{
    using System.Collections.Generic;
    
    using ImageManipulator.Avalonia.Models;
    
    
    public interface IAppService
    {
        /// <summary>
        /// Returns the list of available aspect ratio presets. 
        /// </summary>
        /// <returns>The list of available aspect ratio presets.</returns>
        IList<ImageCropPreset> GetImageCropPresets();
    }
}