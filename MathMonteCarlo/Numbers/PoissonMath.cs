using MathMonteCarlo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.Numbers
{
    internal class PoissonMath
    {

        public static CongruentualNumberGenerator RandomNumberGen = new CongruentualNumberGenerator();

        /// <summary>
        /// Poisson method
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public static int GetPoisson(double lambda)
        {
            // Algorithm due to Donald Knuth, 1969.
            double p = 1.0, L = Math.Exp(-lambda);
            int k = 0;
            do
            {
                k++;
                p *= RandomNumberGen.NextDouble();
            }
            while (p > L);
            return k - 1;
        }

        public static void Test()
        {
            MCViewModel.Log("Poisson tester", "Running 100 poisson random numbers gens around lambda 3.5");
            MCViewModel.Log("Poisson tester", "--------");

            Dictionary<int, int> poissonDict = new Dictionary<int, int>();

            for (int i = 0; i < 1000; i++)
            {
                var poisson = GetPoisson(10);
                
                if (!poissonDict.ContainsKey(poisson)) poissonDict.Add(poisson, 0);
                poissonDict[poisson]++;

                MCViewModel.Log("Poisson tester", poisson.ToString());
            }
            MCViewModel.Log("Poisson tester", "--------");

            foreach (var item in poissonDict.OrderBy(i => i.Key))
            {
                MCViewModel.Log("Poisson tester", $"value {item.Key} : {item.Value} times");
            }
        }
    }
}
