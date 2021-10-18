/* Image Manipulator - (C) 2021 Premysl Fara  */

namespace ImageManipulator.Avalonia.Services
{
    using System.Collections.Generic;
    
    using ImageManipulator.Avalonia.Models;
    
    
    public class AppService: IAppService
    {
        public IList<ImageCropPreset> GetImageCropPresets()
        {
            return new List<ImageCropPreset>()
            {
                new ImageCropPreset(1, 1, 1),
                new ImageCropPreset(2, 3, 2),
                new ImageCropPreset(3, 4, 3),
                new ImageCropPreset(4, 5, 4),
                new ImageCropPreset(5, 7, 5),
                new ImageCropPreset(2, 16, 9),
                new ImageCropPreset(2, 21, 9)
            };
        }
    }
}


/*

            <ComboBoxItem Tag="1x1" IsSelected="True">1:1</ComboBoxItem>
            <ComboBoxItem Tag="3x2">3:2</ComboBoxItem>
            <ComboBoxItem Tag="4x3">4:3</ComboBoxItem>
            <ComboBoxItem Tag="5x4">5:4</ComboBoxItem>
            <ComboBoxItem Tag="7x5">7:5</ComboBoxItem>
            <ComboBoxItem Tag="16x9">16:9</ComboBoxItem>
            <ComboBoxItem Tag="21x9">21:9</ComboBoxItem>
            <ComboBoxItem Tag="custom">Custom</ComboBoxItem>
 
 */