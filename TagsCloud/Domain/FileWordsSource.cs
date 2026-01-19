using System.Collections.Generic;
using System.IO;
using System.Text;
using TagsCloud;
using TagsCloud.Interfaces;

public class FileWordsSource : IWordsSource
{
    private readonly string filePath;
    public FileWordsSource(string filePath) => this.filePath = filePath;
    public Result<List<string>> ReadWords()
    {
        if (!File.Exists(this.filePath))
        {
            return Result<List<string>>.Failure("Не существует файла:" + filePath);
        }
        var words = new List<string>();
        foreach (var line in File.ReadLines(filePath))
        {
            var trimmed = line?.Trim();
            if (!string.IsNullOrEmpty(trimmed)) words.Add(trimmed);
        }
        return Result<List<string>>.Success(words);
    }
}