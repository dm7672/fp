using System.Collections.Generic;
using System.Linq;
using TagsCloud;
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

    public Result<List<string>> Preprocess(Result<List<string>> words)
    {
        if (words.IsFailure)
        {
            return words;
        }
        if(words.Value.Count == 0)
        {
            return Result<List<string>>.Failure("Не обнаружено слов в изначальном файле");
        }
        var lemmas = new List<string>();
        foreach (var w in words.Value)
        {
            if (string.IsNullOrWhiteSpace(w)) continue;

            var analyzed = morphology.Analyze(w); 

            if (stopWordsProvider.IsStopWord(analyzed.Lemma))
                continue;

            if (posFilter.IsAllowed(analyzed.PartOfSpeech))
                lemmas.Add(analyzed.Lemma);
        }
        return Result<List<string>>.Success(lemmas);
    }
}
