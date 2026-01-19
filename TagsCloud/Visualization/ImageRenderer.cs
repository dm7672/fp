// SimpleImageRenderer.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloud.Interfaces;

public class ImageRenderer : IImageRenderer
{
    private readonly Color background;
    private readonly IReadOnlyList<Color> palette;
    private readonly IImageSaver imageSaver;

    public ImageRenderer(Color background, IReadOnlyList<Color> palette, IImageSaver imageSaver)
    {
        this.background = background;
        this.palette = palette;
        this.imageSaver = imageSaver;
    }

    public void Render(string outputPath, IEnumerable<(string Word, Rectangle Rect, float FontSize)> wordRects, int imageWidth, int imageHeight, string fontName)
    {
        
        var rects = wordRects.Select(w => w.Rect).ToList();
        
        var left = rects.Min(r => r.Left);
        var top = rects.Min(r => r.Top);
        var right = rects.Max(r => r.Right);
        var bottom = rects.Max(r => r.Bottom);

        var padding = 20;
        var requiredWidth = Math.Max(100, (right - left) + padding * 2);
        var requiredHeight = Math.Max(100, (bottom - top) + padding * 2);

        var bmpWidth = imageWidth > 0 ? imageWidth : requiredWidth;
        var bmpHeight = imageHeight > 0 ? imageHeight : requiredHeight;

        using var bmp = new Bitmap(bmpWidth, bmpHeight);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.Clear(background);

        var offsetX = -left + padding;
        var offsetY = -top + padding;
        int i = 0;
        foreach (var (word, rect, fontSize) in wordRects)
        {
            var realRect = new Rectangle(rect.Left + offsetX, rect.Top + offsetY, rect.Width, rect.Height);
            using var font = new Font(fontName, Math.Max(1, fontSize));
            var paletteColor = palette[i % palette.Count];
            using var brush = new SolidBrush(paletteColor);

            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(word, font, brush, realRect, sf);
            i++;
        }
        imageSaver.Save(bmp, outputPath);

        Console.WriteLine($"Изображение сохранено в {outputPath}");
    }
}
