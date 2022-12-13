using MathMonteCarlo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.Numbers
{
    internal class Poisson
    {

        public static Generator RandomNumberGen = new Generator(DateTime.Now.Millisecond);
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
            for (int i = 0; i < 100; i++)
            {
                var poisson = GetPoisson(3.5);
                MCViewModel.Log("Poisson tester", poisson.ToString());
            }
        }
    }
}
