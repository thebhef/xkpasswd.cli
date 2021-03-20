using System.Collections.Generic;
using System.Linq;
using xkpasswd.cli;
using Xunit;

namespace Tests.xkpasswd.cli
{
    public class RandomTest
    {
        [Fact]
        public void CoinFlipTest()
        {
            var uut = new RandomSource();

            var iterations = 1000;
            var coinFlipRatios = new List<double>();
            while (iterations-- > -1)
            {
                var flips = Enumerable.Range(0, 200).Select(_ => uut.CoinFlip()).ToList();
                var ratio = flips.Count(r => r) / (double) flips.Count;
                coinFlipRatios.Add(ratio);
                Assert.True(ratio > 0.3, ratio.ToString("0.00%"));
                Assert.True(ratio < 0.7, ratio.ToString("0.00%"));
            }

            var average = coinFlipRatios.Average();
            Assert.True(average > 0.49, average.ToString("0.00%"));
            Assert.True(average < 0.51, average.ToString("0.00%"));
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(int.MinValue, int.MaxValue)]
        [InlineData(int.MaxValue, int.MinValue)]
        public void NextTest(int min, int max)
        {
            var uut = new RandomSource();

            var random = uut.Next(min, max);
            if (max > min)
            {
                Assert.True(random >= min);
                Assert.True(random <= max);
            }
            else
            {
                Assert.True(random >= max);
                Assert.True(random <= min);
            }
        }
    }
}