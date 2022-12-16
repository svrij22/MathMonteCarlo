using MathMonteCarlo.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.Numbers
{
    internal class WeighedRandom
    {
        /// <summary>
        /// Weighed random static function
        /// </summary>
        /// <param name="weighs"></param>
        /// <returns></returns>
        
        public static CongruentualNumberGenerator generator = new CongruentualNumberGenerator();
        public static int GetInt(double[] weighs)
        {
            //Get rand and sum
            double rand = generator.NextDouble();
            double sum = weighs.Sum();

            //Convert to fractions
            double[] wFractions = weighs.Select(x => x / sum)
                .ToArray();

            //For each fraction
            for (int i = 0; i < wFractions.Length; i++)
            {
                //Update bounds
                double leftBound = wFractions.Take(i).Sum();
                double rightBound = wFractions.Take(i + 1).Sum();

                //In bound? return int
                if (rand >= leftBound && rand <= rightBound)
                    return i;
            }

            return -1;
        }

        public static T GetEnum<T>(double[] weighs) where T : Enum
        {
            var intv = GetInt(weighs);
            var value = (T)(object)intv;
            return value;
        }

        public static void Test()
        {
            MCViewModel.Log("Weighed Random Tests", "Running Weighed Random tests");
            MCViewModel.Log("Weighed Random Tests", "Weighs are { 50, 30, 15, 5 }");
            MCViewModel.Log("Weighed Random Tests", "Amount of items in 10000 should be around 5000, 3000, 1500 and 500");

            //Set weighs
            double[] weighs = new double[] { 50, 30, 15, 5 };
            List<int> res = new List<int>();

            //Run sim
            for (int i = 0; i < 10000; i++)
            {
                //add to result
                res.Add(GetInt(weighs.ToArray()));
            }

            MCViewModel.Log("Weighed Random Tests", "amount of '50' : " + res.Count(r => r == 0));
            MCViewModel.Log("Weighed Random Tests", "amount of '30' : " + res.Count(r => r == 1));
            MCViewModel.Log("Weighed Random Tests", "amount of '15' : " + res.Count(r => r == 2));
            MCViewModel.Log("Weighed Random Tests", "amount of '5' : " + res.Count(r => r == 3));
        }

    }
}
