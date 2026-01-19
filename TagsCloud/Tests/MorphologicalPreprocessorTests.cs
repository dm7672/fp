using System.IO;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloud.Interfaces;
using TagsCloud.Domain;
using TagsCloud.Domain.Model;
namespace TagsCloud.Tests
{
    [TestFixture]
    public class MorphologicalPreprocessorTests
    {
        private Mock<IMorphologyAnalyzer> morphologyMock;
        private DefaultPartOfSpeechFilter posFilter;

        [SetUp]
        public void SetUp()
        {
            morphologyMock = new Mock<IMorphologyAnalyzer>(MockBehavior.Strict);
            posFilter = new DefaultPartOfSpeechFilter();
        }

        [TearDown]
        public void TearDown()
        {
            morphologyMock.VerifyAll();
        }

        [Test]
        public void Preprocess_WhitespaceOnly_DoesNotCallMorphologyAndReturnsEmpty()
        {
            var stopProvider = new FileStopWordsProvider("nonexistent-stopwords.txt"); // file missing => empty provider
            var pre = new MorphologicalPreprocessor(morphologyMock.Object, posFilter, stopProvider);

            var result = pre.Preprocess(new[] { "", "   ", "\t" }).ToList();

            result.Should().BeEmpty();
            morphologyMock.Verify(m => m.Analyze(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Preprocess_StopWordExcluded_ResultIsEmpty()
        {
            // prepare stopwords file with "hello"
            var tmp = Path.GetTempFileName();
            File.WriteAllText(tmp, "hello\n");

            var stopProvider = new FileStopWordsProvider(tmp);

            // morphology returns lemma "hello" for token "Hello"
            morphologyMock.Setup(m => m.Analyze("Hello"))
                          .Returns(new Word("Hello", "hello", PartOfSpeech.Noun));

            var pre = new MorphologicalPreprocessor(morphologyMock.Object, posFilter, stopProvider);

            var result = pre.Preprocess(new[] { "Hello" }).ToList();

            result.Should().BeEmpty();

            File.Delete(tmp);
        }

        [Test]
        public void Preprocess_PosurationFilter_ExcludesDisallowedPartOfSpeech()
        {
            var stopProvider = new FileStopWordsProvider("nonexistent-stopwords.txt");

            // morphology returns lemma with Pronoun POS, DefaultPartOfSpeechFilter does not allow Pronoun
            morphologyMock.Setup(m => m.Analyze("я"))
                          .Returns(new Word("я", "я", PartOfSpeech.Pronoun));

            var pre = new MorphologicalPreprocessor(morphologyMock.Object, posFilter, stopProvider);

            var result = pre.Preprocess(new[] { "я" }).ToList();

            result.Should().BeEmpty();
        }

        [Test]
        public void Preprocess_AllowsAllowedPartOfSpeechAndNotStopWord_ReturnsLemma()
        {
            var stopProvider = new FileStopWordsProvider("nonexistent-stopwords.txt");

            // morphology returns lemma "run" and Verb; Default filter allows Verb
            morphologyMock.Setup(m => m.Analyze("Running"))
                          .Returns(new Word("Running", "run", PartOfSpeech.Verb));

            var pre = new MorphologicalPreprocessor(morphologyMock.Object, posFilter, stopProvider);

            var result = pre.Preprocess(new[] { "Running" }).ToList();

            result.Should().Equal(new[] { "run" });
        }
    }
}
