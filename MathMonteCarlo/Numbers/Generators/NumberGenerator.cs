using System;

namespace MathMonteCarlo.Numbers
{
    public abstract class NumberGenerator
    {
        public long LongSeedFromDateTime()
        {
            return long.Parse($"{DateTime.Now.Year}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Month}{DateTime.Now.Second}{DateTime.Now.Millisecond}");
        }
        public int IntSeedFromDateTime()
        {
            return int.Parse($"{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Month}{DateTime.Now.Second}{DateTime.Now.Millisecond}");
        }

        public abstract double NextDouble();
    }
}