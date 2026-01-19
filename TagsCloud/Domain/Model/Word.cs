using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.Domain.Model
{
    public sealed record Word(
    string Text,
    string Lemma,
    PartOfSpeech PartOfSpeech);
}
