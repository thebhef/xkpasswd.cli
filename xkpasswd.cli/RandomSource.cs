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
            var randomScale = BitConverter.ToUInt32(randomBytes) / (double) uint.MaxValue;
            return (int) (randomScale * (maxValue - minValue) + minValue);
        }


        public bool CoinFlip() => Next(0, 100) > 49;
    }
}