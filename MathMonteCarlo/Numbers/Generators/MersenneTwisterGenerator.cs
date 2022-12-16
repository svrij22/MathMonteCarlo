using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.Numbers.Generators
{
    //Kijk in slides
    internal class MersenneTwisterGenerator : NumberGenerator
    {
        public int u = 4;
        public int s = 4;
        public int t = 4;
        public int l = 4;

        public int b = 0b_1011_0110_1011_0110;
        public int c = 0b_0011_0111_0001_1000;

        public int bits = 16;

        public int mod;
        public MersenneTwisterGenerator()
        {
            mod = (int)Math.Pow(2, bits);
        }

        public int n = 5; //amount of items

        public List<int> MT = new()
        {
            0b_1101_0101_1111_1111,
            0b_0111_1011_0110_0110,
            0b_0110_1011_0110_0110,
            0b_1111_0101_1100_1011,
            0b_1100_1100_1100_0110,
        };
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
                //new x
                var x = (MT[i] & upper_mask) + MT[(i + 1) % n] & lower_mask;

                //shift
                var xA = x >> 1;

                //if its uneven
                if ((x % 2) != 0)
                    xA = xA ^ a;

                //set
                MT[i] = (int)(MT[(i + m) % n] ^ xA);
            }
            index = 0;
        }
    }
}
