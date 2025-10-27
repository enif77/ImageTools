/* Copyright (C) Premysl Fara and Contributors */

namespace ImageTools.Core.PixelInterpolators;

using System;

/// <summary>
/// Image pixel interpolator using the nearest neighbor strategy.
/// </summary>
public class NearestPixelInterpolator : APixelInterpolator
{
    public override void CopyPixel(ImageData inputImage, ImageData outputImage, double xFrom, double yFrom, int to)
    {
        var nearest = GetReadIndex(
            (int)Clamp(Math.Round(xFrom), 0, inputImage.Width - 1),
            (int)Clamp(Math.Round(yFrom), 0, inputImage.Height - 1),
            inputImage.Width);

        for (var channel = 0; channel < 3; channel++)
        {
            outputImage.Data[to + channel] = inputImage.Data[nearest + channel];
        }
    }
}
