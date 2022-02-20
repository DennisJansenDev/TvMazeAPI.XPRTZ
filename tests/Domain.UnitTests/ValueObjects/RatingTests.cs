using Domain.ValueObjects;
using System;
using Xunit;

namespace Domain.UnitTests.ValueObjects
{
    public class RatingTests
    {
        [Theory]
        [InlineData(10.1)]
        [InlineData(0.9)]
        public void ShouldThrowArgumentOutOfRangeException(double outOfRangeValue)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Rating.FromDouble(outOfRangeValue));
        }

        [Theory]
        [InlineData(10.0)]
        [InlineData(1.0)]
        public void ShouldReturnValidRatingObject(double expectedAverage)
        {
            var result = Rating.FromDouble(expectedAverage);
            Assert.Equal(expectedAverage, result.Average);
        }

    }
}
