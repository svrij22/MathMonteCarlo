using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.Numbers.Generators
{
    //Kijk in slides
    internal class MersenneTwisterGenerator
    {
        public int u = 1;
        public int s = 1;
        public int t = 1;
        public int l = 1;

        public byte b = 0b_1010;
        public byte c = 0b_0011;

        public List<byte> x = new()
        {
            0b_1101,
            0b_0111,
            0b_0110,
            0b_1111,
            0b_1100,
        };
        public byte y1()
        {
            byte y1;
            y1 = (byte)(x[0] ^ (x[0] >> u));
            y1 = (byte)(y1   ^ (y1   << s));
            y1 = (byte)(y1   ^ (y1   << t));
            y1 = (byte)(y1   ^ (y1   >> l));

            return y1;
        }
    }
}
