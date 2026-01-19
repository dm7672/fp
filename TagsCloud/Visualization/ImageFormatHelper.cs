using System.Drawing.Imaging;

public static class ImageFormatHelper
{
    public static ImageFormat Parse(string format)
    {
        if (string.IsNullOrWhiteSpace(format))
            return ImageFormat.Png;

        format = format.Trim().ToLowerInvariant();
        return format switch
        {
            "png" => ImageFormat.Png,
            "jpg" or "jpeg" => ImageFormat.Jpeg,
            "bmp" => ImageFormat.Bmp,
            "gif" => ImageFormat.Gif,
            "tiff" => ImageFormat.Tiff,
            _ => ImageFormat.Png
        };
    }
}
