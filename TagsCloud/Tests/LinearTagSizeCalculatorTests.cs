using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Domain;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class LinearTagSizeCalculatorTests
    {
        [Test]
        public void CalculateFontSize_WhenMinEqualsMax_ReturnsAverage()
        {
            var calc = new LinearTagSizeCalculator(12f, 12f);

            var size = calc.CalculateFontSize(frequency: 5, minFrequency: 1, maxFrequency: 1);

            size.Should().BeApproximately(12f, 1e-6f);
        }

        [Test]
        public void CalculateFontSize_AtMinimumFrequency_ReturnsMinFont()
        {
            var calc = new LinearTagSizeCalculator(8f, 24f);

            var size = calc.CalculateFontSize(frequency: 1, minFrequency: 1, maxFrequency: 5);

            size.Should().BeApproximately(8f, 1e-6f);
        }

        [Test]
        public void CalculateFontSize_AtMaximumFrequency_ReturnsMaxFont()
        {
            var calc = new LinearTagSizeCalculator(8f, 24f);

            var size = calc.CalculateFontSize(frequency: 5, minFrequency: 1, maxFrequency: 5);

            size.Should().BeApproximately(24f, 1e-6f);
        }

        [Test]
        public void CalculateFontSize_MiddleFrequency_ReturnsInterpolatedValue()
        {
            var calc = new LinearTagSizeCalculator(10f, 50f);

            var size = calc.CalculateFontSize(frequency: 3, minFrequency: 1, maxFrequency: 5);

            size.Should().BeApproximately(30f, 1e-6f);
        }
    }
}
