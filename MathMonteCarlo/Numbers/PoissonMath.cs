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

        /// <summary>
        /// Custom implementation
        /// </summary>
        /// <returns></returns>
        public static int GetPoissionCustom(double lambda)
        {
            //Poisson functie
            Func<int, double> poisson = (k) =>
            {
                var var1 = Math.Pow(lambda, k);
                var var2 = factorial(k);
                var var3 = Math.Pow(Math.E, -lambda);
                return (var1 / var2) * var3;
            };

            //Create odds
            var maxAmountOfGoals = 12;
            List<double> odds = new();  
            for (int i = 0; i < maxAmountOfGoals; i++)
            {
                odds.Add(poisson(i));
            }

            //weighed randomizer ( to make up for the few %% we're missing at the top)
            var res = WeighedRandom.GetInt(odds.ToArray());

            return res;
        }

        public static int factorial(int fact)
        {
            //In python 1 gets wrapped up to 2
            fact = Math.Max(fact, 1);
            int a = fact;
            for (int x = 1; x < a; x++)
                fact *= x;
            return fact;
        }

        public static void Test()
        {
            MCViewModel.Log("Poisson tester", "Running 100 poisson random numbers gens around lambda 3.5");
            MCViewModel.Log("Poisson tester", "--------");

            Dictionary<int, int> poissonDict = new Dictionary<int, int>();

            for (int i = 0; i < 1000; i++)
            {
                var poisson = GetPoisson(3.5);
                
                if (!poissonDict.ContainsKey(poisson)) poissonDict.Add(poisson, 0);
                poissonDict[poisson]++;

                MCViewModel.Log("Poisson tester", poisson.ToString());
            }
            MCViewModel.Log("Poisson tester", "--------");

            foreach (var item in poissonDict.OrderBy(i => i.Key))
            {
                MCViewModel.Log("Poisson tester", $"value {item.Key} : {item.Value} times");
            }

            ///CUSTOM 
            ///

            MCViewModel.Log("Poisson tester (custom)", "Running 100 poisson random numbers gens around lambda 3.5");
            MCViewModel.Log("Poisson tester (custom)", "--------");
            Dictionary<int, int> poissonDict2 = new Dictionary<int, int>();

            //get poisson nrs
            for (int i = 0; i < 1000; i++)
            {
                var poisson = GetPoissionCustom(3.5);
                if (!poissonDict2.ContainsKey(poisson)) poissonDict2.Add(poisson, 0);
                poissonDict2[poisson]++;
                MCViewModel.Log("Poisson tester (custom)", poisson.ToString());
            }

            MCViewModel.Log("Poisson tester (custom)", "--------");
            foreach (var item in poissonDict2.OrderBy(i => i.Key))
                MCViewModel.Log("Poisson tester (custom)", $"value {item.Key} : {item.Value} times");
        }
    }
}
