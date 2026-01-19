using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloud.Interfaces;
public class FileStopWordsProvider : IStopWordsProvider
{
    private readonly HashSet<string> stopWords;
    public FileStopWordsProvider(string stopWordsFile)
    {
        stopWords = File.Exists(stopWordsFile)
            ? new HashSet<string>(File.ReadLines(stopWordsFile).Select(s => s.Trim().ToLowerInvariant()))
            : new HashSet<string>();
    }
    public bool IsStopWord(string word) => stopWords.Contains(word.ToLowerInvariant());
}