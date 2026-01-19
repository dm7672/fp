using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

public sealed class MorphologicalPreprocessor : IWordsPreprocessor
{
    private readonly IMorphologyAnalyzer morphology;
    private readonly IPartOfSpeechFilter posFilter;
    private readonly IStopWordsProvider stopWordsProvider;

    public MorphologicalPreprocessor(IMorphologyAnalyzer morphology, IPartOfSpeechFilter posFilter, IStopWordsProvider stopWordsProvider)
    {
        this.morphology = morphology;
        this.posFilter = posFilter;
        this.stopWordsProvider = stopWordsProvider;
    }

    public IEnumerable<string> Preprocess(IEnumerable<string> words)
    {
        foreach (var w in words)
        {
            if (string.IsNullOrWhiteSpace(w)) continue;

            var analyzed = morphology.Analyze(w); 

            if (stopWordsProvider.IsStopWord(analyzed.Lemma))
                continue;

            if (posFilter.IsAllowed(analyzed.PartOfSpeech))
                yield return analyzed.Lemma;
        }
    }
}
