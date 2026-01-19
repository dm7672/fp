using System.Collections.Generic;
using System.Linq;
using TagsCloud;
using TagsCloud.Interfaces;
public class FrequencyAnalyzer : IFrequencyAnalyzer
{
    public Result<IDictionary<string, int>> CountFrequencies(Result<List<string>> words)
    {
        if(words.IsFailure)
        {
            return Result<IDictionary<string, int>>.Failure(words.Error); 
        }
        if(words.Value.Count == 0) { 
            return Result<IDictionary<string, int>>.Failure("Не обнаружено слов для визуализации");
        }
        var dict = new Dictionary<string, int>();
        foreach (var w in words.Value)
        {
            if (dict.ContainsKey(w)) dict[w]++; else dict[w] = 1;
        }
        return Result<IDictionary<string, int>>.Success(dict);
    }
}