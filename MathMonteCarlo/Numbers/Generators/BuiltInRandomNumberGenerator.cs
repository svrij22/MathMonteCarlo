using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.Numbers
{
    internal class BuiltInRandomNumberGenerator : NumberGenerator
    {
        public Random random = new();
        public BuiltInRandomNumberGenerator(int seed = 0)
        {
            random = new(seed);
        }
        public override double NextDouble()
        {
            return random.NextDouble();
        }
    }
}
