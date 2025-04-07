using System;

namespace Zadanie8
{
    public static class RandomGenerator
    {
        private static readonly Random _random = new Random();

        public static double GetNextDouble()
        {
            return _random.NextDouble();
        }
    }
}
