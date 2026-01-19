using System.Collections.Generic;
using System.IO;
using System.Text;
using TagsCloud.Interfaces;

public class FileWordsSource : IWordsSource
{
    private readonly string filePath;
    public FileWordsSource(string filePath) => this.filePath = filePath;
    public IEnumerable<string> ReadWords()
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var trimmed = line?.Trim();
            if (!string.IsNullOrEmpty(trimmed)) yield return trimmed;
        }
    }
}