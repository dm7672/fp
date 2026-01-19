using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;
public class FrequencyAnalyzer : IFrequencyAnalyzer
{
    public IDictionary<string, int> CountFrequencies(IEnumerable<string> words)
    {
        var dict = new Dictionary<string, int>();
        foreach (var w in words)
        {
            if (dict.ContainsKey(w)) dict[w]++; else dict[w] = 1;
        }
        return dict;
    }
}