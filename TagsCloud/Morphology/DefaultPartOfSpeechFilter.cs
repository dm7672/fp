using System.Collections.Generic;
using TagsCloud.Interfaces;
using TagsCloud.Domain.Model;

public sealed class DefaultPartOfSpeechFilter : IPartOfSpeechFilter
{
    private static readonly HashSet<PartOfSpeech> allowed = new()
    {
        PartOfSpeech.Noun,
        PartOfSpeech.Adjective,
        PartOfSpeech.Verb
    };

    public bool IsAllowed(PartOfSpeech pos) => allowed.Contains(pos);
}