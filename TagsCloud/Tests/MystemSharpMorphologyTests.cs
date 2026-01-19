using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Domain.Model;
using TagsCloud.Morphology;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class MystemSharpMorphologyTests
    {

        [Test]
        public void Analyze_Noun_ReturnsNounAndNormalizedLemma()
        {
            var morph = new MystemSharpMorphology();

            var res = morph.Analyze("коты");

            res.PartOfSpeech.Should().Be(PartOfSpeech.Noun);
            res.Lemma.Should().Be("кот");
        }

        [Test]
        public void Analyze_Verb_ReturnsVerbAndInfinitiveLemma()
        {
            var morph = new MystemSharpMorphology();

            var res = morph.Analyze("бежал");

            res.PartOfSpeech.Should().Be(PartOfSpeech.Verb);
            res.Lemma.Should().Be("бежать");
        }

        [Test]
        public void Analyze_Adjective_ReturnsAdjectiveAndMasculineLemma()
        {
            var morph = new MystemSharpMorphology();

            var res = morph.Analyze("красивые");

            res.PartOfSpeech.Should().Be(PartOfSpeech.Adjective);
            res.Lemma.Should().Be("красивый");
        }

        [Test]
        public void Analyze_Adverb_ReturnsAdverbAndSameLemma()
        {
            var morph = new MystemSharpMorphology();

            var res = morph.Analyze("быстро");

            res.PartOfSpeech.Should().Be(PartOfSpeech.Adverb);
            res.Lemma.Should().Be("быстро");
        }

        [Test]
        public void Analyze_Pronoun_ReturnsPronoun()
        {
            var morph = new MystemSharpMorphology();

            var res = morph.Analyze("он");

            res.PartOfSpeech.Should().Be(PartOfSpeech.Pronoun);
            res.Lemma.Should().Be("он");
        }

        [Test]
        public void Analyze_Preposition_ReturnsPreposition()
        {
            var morph = new MystemSharpMorphology();

            var res = morph.Analyze("под");

            res.PartOfSpeech.Should().Be(PartOfSpeech.Preposition);
            res.Lemma.Should().Be("под");
        }

        [Test]
        public void Analyze_Conjunction_ReturnsConjunction()
        {
            var morph = new MystemSharpMorphology();

            var res = morph.Analyze("и");

            res.PartOfSpeech.Should().Be(PartOfSpeech.Conjunction);
            res.Lemma.Should().Be("и");
        }

        [Test]
        public void Analyze_Particle_ReturnsParticle()
        {
            var morph = new MystemSharpMorphology();

            var res = morph.Analyze("же");

            res.PartOfSpeech.Should().Be(PartOfSpeech.Particle);
            res.Lemma.Should().Be("же");
        }

        [Test]
        public void Analyze_UnknownToken_ReturnsOther()
        {
            var morph = new MystemSharpMorphology();

            var res = morph.Analyze("qwerty123");

            res.PartOfSpeech.Should().Be(PartOfSpeech.Other);
            res.Lemma.Should().NotBeNullOrWhiteSpace();
        }
    }
}
