using TagsCloud.Interfaces;
namespace TagsCloud.Domain;
public class LinearTagSizeCalculator : ITagSizeCalculator
{
    private readonly float minFont;
    private readonly float maxFont;
    public LinearTagSizeCalculator(float minFont, float maxFont)
    {
        this.minFont = minFont;
        this.maxFont = maxFont;
    }

    public float CalculateFontSize(int frequency, int minFrequency, int maxFrequency)
    {
        if (minFrequency == maxFrequency) return (minFont + maxFont) / 2;
        var t = (frequency - minFrequency) / (float)(maxFrequency - minFrequency);
        return minFont + t * (maxFont - minFont);
    }
}