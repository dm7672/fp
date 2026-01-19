using System.Drawing;
using System.Drawing.Imaging;

public interface IImageSaver
{
    void Save(Image image, string outputPath);
}