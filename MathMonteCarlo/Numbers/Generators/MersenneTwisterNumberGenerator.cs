using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.Numbers.Generators
{
    //Kijk in slides
    //Simpele implementatie maar werkt wel
    //wat doet f?
    internal class MersenneTwisterNumberGenerator : NumberGenerator
    {

        //shifting params
        public int u = 2;
        public int s = 3;
        public int t = 2;
        public int l = 1;

        //masks
        public int b = 0b_1011_0110_1011_0110;
        public int c = 0b_0011_0111_0001_1000;

        //amount of bits
        public int bits = 16;

        //max integer size
        public int mod;
        public MersenneTwisterNumberGenerator(int seed = 0)
        {
            //Get seed
            if (seed == 0) seed = IntSeedFromDateTime();

            //Generate
            genseed(seed);

            //init max integer size
            mod = (int)Math.Pow(2, bits);
        }

        //amount of items
        public int n = 32; 

        //seed array
        public List<int> MT = new();

        //magic number f ???
        int f = 1812433253;

        //generate initial numbers
        public void genseed(int seed)
        {
            MT.Add(seed);
            for (int i = 1; i < n; i++)
            {
                var temp = f * (MT[i - 1] ^ (MT[i - 1] >> (n - 2))) + i;
                MT.Add((int)(temp & 0xffffffff));
            }
        }
        public int genran()
        {
            //generate y and new x
            var y1 = extract_number();

            //return
            return y1;
        }

        public override double NextDouble()
        {
            // mod = max integer size

            return (genran() / (double)mod);
        }

        public int index = 0;
        public int extract_number()
        {
            if (index >= n-1)
            {
                twist();
            }

            int _y = MT[index];

            //bitwise shifting for extracted number

            _y = _y ^ (_y >> u);
            _y = _y % mod;

            _y = _y ^ ((_y << s) & b);
            _y = _y % mod;

            _y = _y ^ ((_y << t) & c);
            _y = _y % mod;

            _y = _y ^ (_y >> l);
            _y = _y % mod;

            index++;

            return _y;
        }

        uint a = 0x9908B0DF;

        uint lower_mask = 0x7FFFFFFF;
        uint upper_mask = 0x80000000;

        int m = 397;
        public void twist()
        {
            //for each mt number
            for (int i = 0; i < n; i++)
            {
                //TWIST
                var x = (MT[i] & upper_mask) + MT[(i + 1) % n] & lower_mask;

                //shift
                var xA = x >> 1;

                //if its uneven
                if ((x % 2) != 0)
                    xA = xA ^ a;

                //set
                MT[i] = (int)(MT[(i + m) % n] ^ xA);
            }

            //reset index
            index = 0;
        }
    }
}
