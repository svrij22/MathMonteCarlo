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

        public List<int> x = new()
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
            var y1 = calcy(x[0]);
            var x6 = calcx(x[0],  x[1], x[2]);

            //push to list
            x.Add(x6);
            x = x.Skip(1).ToList();

            //return
            return y1;
        }

        public override double NextDouble()
        {
            return (genran() / (double)mod);
        }

        public int calcx(int base_x1, int base_x2, int base_x3)
        {
            int _firstMask = mod/2;
            byte firstByte1 = (byte)(base_x1 & _firstMask);
            //Debug.WriteLine(Convert.ToString(firstByte1, toBase: 2));

            int _secondMask = (mod/2) - 1;
            int threeBytes2 = (byte)(base_x2 & _secondMask);
            //Debug.WriteLine(Convert.ToString(threeBytes2, toBase: 2));

            var xnew = base_x3 ^ (firstByte1 + threeBytes2);
            return xnew;
        }
        public int calcy(int _base)
        {
            int _y = _base;

            _y = _y ^ (_y >> u);
            _y = _y % mod;
            //Debug.WriteLine(Convert.ToString(_y, toBase: 2));

            _y = _y ^ ((_y << s) & b);
            _y = _y % mod;
            //Debug.WriteLine(Convert.ToString(_y, toBase: 2));

            _y = _y ^ ((_y << t) & c);
            _y = _y % mod;
            //Debug.WriteLine(Convert.ToString(_y, toBase: 2));

            _y = _y ^ (_y >> l);
            _y = _y % mod;
            //Debug.WriteLine(Convert.ToString(_y, toBase: 2));

            return _y;
        }
    }
}
