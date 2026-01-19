using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class FrequencyAnalyzerTests
    {
        [Test]
        public void CountFrequencies_WithMultipleOccurrences_ReturnsCorrectCounts()
        {
            var analyzer = new FrequencyAnalyzer();
            var input = new[] { "a", "b", "a", "c", "b", "a" };

            var result = analyzer.CountFrequencies(input);

            result.Should().ContainKey("a").WhoseValue.Should().Be(3);
            result.Should().ContainKey("b").WhoseValue.Should().Be(2);
            result.Should().ContainKey("c").WhoseValue.Should().Be(1);
            result.Count.Should().Be(3);
        }

        [Test]
        public void CountFrequencies_WithEmptyInput_ReturnsEmptyDictionary()
        {
            var analyzer = new FrequencyAnalyzer();

            var result = analyzer.CountFrequencies(new string[0]);

            result.Should().NotBeNull().And.BeEmpty();
        }

        [Test]
        public void CountFrequencies_WithSingleWord_ReturnsCountOne()
        {
            var analyzer = new FrequencyAnalyzer();

            var result = analyzer.CountFrequencies(new[] { "word" });

            result.Should().ContainKey("word").WhoseValue.Should().Be(1);
            result.Count.Should().Be(1);
        }
    }
}
