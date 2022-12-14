using System;

namespace MathMonteCarlo.Numbers
{
    public abstract class NumberGenerator
    {
        public long SeedFromDateTime(long seed)
        {
            return long.Parse($"{DateTime.Now.Year}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Month}{DateTime.Now.Second}{DateTime.Now.Millisecond}");
        }

        public abstract double NextDouble();
    }
}