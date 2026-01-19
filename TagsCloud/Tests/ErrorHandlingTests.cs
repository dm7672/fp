using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Testing.Platform.Requests;
using Moq;
using NUnit.Framework;
using TagsCloud;
using TagsCloud.Interfaces;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class ErrorHandlingTests
    {
        [Test]
        public void FileWordsSource_ReturnsError_WhenFilePathIsWrong()
        {
            var wordsSource = new FileWordsSource("Несуществующий путь");
            
            var result = wordsSource.ReadWords();

            result.IsFailure.Should().BeTrue();
        }

        [Test]
        public void MorphologicalPreprocessor_ReturnsError_WhenWordsCountIsZero()
        {
            var morphologyMock = new Mock<IMorphologyAnalyzer>(MockBehavior.Strict);
            var posFilter = new DefaultPartOfSpeechFilter();
            var stopProvider = new FileStopWordsProvider("");

            var preprocessor = new MorphologicalPreprocessor(morphologyMock.Object, posFilter, stopProvider);

            var result = preprocessor.Preprocess(Result<List<string>>.Success(new List<string>()));

            result.IsFailure.Should().BeTrue();
        }

        [Test]
        public void FrequencyAnalyzer_ReturnsError_WhenWordsCountIsZero()
        {

            var freqAnalyzer = new FrequencyAnalyzer();

            var result = freqAnalyzer.CountFrequencies(Result<List<string>>.Success(new List<string>()));

            result.IsFailure.Should().BeTrue();
        }
    }
}
