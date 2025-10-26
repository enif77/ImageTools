# ImageTools

Various images manipulation tools.

## Transformation - Crop

Crops image into a defined aspect ratio using the largest possible central part of it.

- AspectRatioX: The X value of the desired output aspect ratio (double, 0 < X <= Double.Max).
- AspectRatioY: The Y value of the desired output aspect ratio (double, 0 < Y <= Double.Max).

Its applied first, before resizing.

## Transformation - Resize

Resizes an image so it matches the output ratio and none of its sides is bigger than a specified side size in pixels.

- MaxSideSize: The maximal side size in pixels (0 < S <= Int32.Max).

It's applied after cropping.

## Output format

Defines a file format of the output image.

### JPEG

The default format.

- JpegMaxQuality: The maximal JPEG image quality (0 < Q <= 100). Its 95 by default.
- JpegMaxFileSize: The maximal output file size in Bytes (S <= Int32.Max). Its 0 (no limit) by default.

The MaxFileSize is used to find a JPEG quality, that produces a JPEG with a size in Bytes,
that is small enough to fit into the maximal file size specified. The resulting file can be
bigger, if such maximal file size cannot be reached. If the MaxFileSize is <= 0,
then no file size limit is in place. 

### PNG

Produces a file in the PNG file format.

- PngCompressionLevel: The compression level to be used for saving the PNG image.
