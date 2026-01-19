using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public sealed class GenericImageSaver : IImageSaver
{
    private readonly ImageFormat format;

    public GenericImageSaver(ImageFormat format)
    {
        this.format = format;
    }

    public void Save(Image image, string outputPath)
    {
        var dir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);

        image.Save(outputPath, format);
    }
}
