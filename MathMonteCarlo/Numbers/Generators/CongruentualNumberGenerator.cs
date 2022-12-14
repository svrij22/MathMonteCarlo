using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.Numbers
{
    internal class CongruentualNumberGenerator : NumberGenerator
    {
        public double A;
        public double C;
        public double M;

        public double Current;
        public CongruentualNumberGenerator(long seed = 0)
        {
            if (seed == 0) seed = SeedFromDateTime(seed);
            A = (seed / 10); 
            C = 5 + (seed / 10); 
            M = seed;
        }
        /// <summary>
        /// Returns a value between 0 and 1
        /// </summary>
        /// <returns></returns>
        public override double NextDouble()
        {
            A++; C++; M += 2;  
            var Next = (Current * A) + C;
            Next = Next % M;
            Next = Math.Abs(Next);
            Current = Next;
            return (Next / M);
        }
    }
}
