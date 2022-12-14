using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.Numbers
{
    internal class MidSquareNumberGenerator : NumberGenerator
    {
        public BigInteger Key = 0;
        public MidSquareNumberGenerator(long seed = 0)
        {
            if (seed == 0) seed = SeedFromDateTime(seed);

            //Set key
            Key = seed;
        }

        public void NextKey()
        {
            //Loop until large enough
            while(Key < (BigInteger)1e16)
            {
                Key = (BigInteger)Math.Abs((decimal)(Key + 1)); // Dont get stuck on <0
                Key = BigInteger.Pow(Key, 2);
            }

            //Get middle part
            var newKey = Key % (BigInteger)1e15;
            newKey = newKey / (BigInteger)1e7;

            //Set key, value between 0 - 99.999.999
            Key = newKey;
        }

        public override double NextDouble()
        {
            NextKey();
            return (double)Key / 99999999d;
        }
    }
}
