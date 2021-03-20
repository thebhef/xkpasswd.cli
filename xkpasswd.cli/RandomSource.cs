using System;
using System.Security.Cryptography;
using XkPassword;

namespace xkpasswd.cli
{
    public class RandomSource : IRandomSource
    {
        private readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

        public int Next(int minValue, int maxValue)
        {
            var randomBytes = new byte[4];
            _random.GetNonZeroBytes(randomBytes);
            var clippedMin = minValue == 0 ? 1 : minValue;
            var randomScale = BitConverter.ToUInt32(randomBytes) / (double) uint.MaxValue;
            return (int) (randomScale * (maxValue - clippedMin) + clippedMin);
        }


        public bool CoinFlip() => Next(0, 100) > 49;
    }
}