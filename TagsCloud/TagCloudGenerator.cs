using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud;
using TagsCloud.Interfaces;

public class TagCloudGenerator : ITagCloudGenerator
{
    private readonly IWordsSource wordsSource;
    private readonly IWordsPreprocessor preprocessor;
    private readonly IFrequencyAnalyzer freqAnalyzer;
    private readonly ITagSizeCalculator sizeCalculator;
    private readonly Func<ITagLayouter> layouterFactory;
    private readonly IImageRenderer renderer;

    public TagCloudGenerator(
        IWordsSource wordsSource,
        IWordsPreprocessor preprocessor,
        IFrequencyAnalyzer freqAnalyzer,
        ITagSizeCalculator sizeCalculator,
        Func<ITagLayouter> layouterFactory,
        IImageRenderer renderer)
    {
        this.wordsSource = wordsSource;
        this.preprocessor = preprocessor;
        this.freqAnalyzer = freqAnalyzer;
        this.sizeCalculator = sizeCalculator;
        this.layouterFactory = layouterFactory;
        this.renderer = renderer;
    }

    public Result<None> Generate(string outputPath, int imageWidth, int imageHeight, string fontName)
    {

        var rawWords = wordsSource.ReadWords();
        var words = preprocessor.Preprocess(rawWords);
        var freqs = freqAnalyzer.CountFrequencies(words);
        if (freqs.IsFailure) { return Result<None>.Failure(freqs.Error); }

        var minFreq = freqs.Value.Values.Min();
        var maxFreq = freqs.Value.Values.Max();

        var layouter = layouterFactory();

        using var tmpBmp = new Bitmap(1, 1);
        using var g = Graphics.FromImage(tmpBmp);

        var wordRects = new List<(string Word, Rectangle Rect, float FontSize)>();
        var rnd = new Random(0);
        foreach (var kv in freqs.Value.OrderByDescending(p => p.Value))
        {
            var word = kv.Key;
            var freq = kv.Value;
            var fontSize = sizeCalculator.CalculateFontSize(freq, minFreq, maxFreq);
            using var font = new Font(fontName, fontSize);
            var sizeF = g.MeasureString(word, font);
            var size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
            var rect = layouter.PutNextRectangle(size);
            wordRects.Add((word, rect, fontSize));
        }

        renderer.Render(outputPath, wordRects, imageWidth, imageHeight, fontName);

        return Result<None>.Success(null);
    }


}