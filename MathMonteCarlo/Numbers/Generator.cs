using MathMonteCarlo.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.Numbers
{
    internal class Generator
    {
        public double A;
        public double C;
        public double M;

        public double Current;
        public Generator(int seed)
        {
            seed += 442312341;
            A = (seed / 10); 
            C = 5 + (seed / 10); 
            M = seed;
        }
        /// <summary>
        /// Returns a value between 0 and 1
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            A++; C++; M += 2;  
            var Next = (Current * A) + C;
            Next = Next % M;
            Next = Math.Abs(Next);
            Current = Next;
            return (Next / M);
        }
    }

    /// <summary>
    /// Tests the generator
    /// </summary>
    internal class GeneratorTester
    {
        public static void Test()
        {
            MCViewModel.Log("Number Generator Tests", "Running Number generator tests");
            MCViewModel.Log("Number Generator Tests", "Average should be .5");
            MCViewModel.Log("Number Generator Tests", "Amount of items in 1000 should be around 500");

            Generator gen = new Generator(DateTime.Now.Millisecond);

            MCViewModel.Log("Number Generator Tests", $"Generator uses current millisecond as seed");
            MCViewModel.Log("Number Generator Tests", $"For seed : {DateTime.Now.Millisecond}");

            // Number of tests to run
            for (int i = 0; i < 20; i++)
            {
                // Amount of numbers under 50%
                int amountOfNo50 = 0;

                // Average
                double absoluteAverage = 0;

                //Amount of numbers to generate
                int amountOfNumbers = 1000;

                for (int j = 0; j < amountOfNumbers; j++)
                {
                    var number = gen.NextDouble();

                    if (number <= .5)
                        amountOfNo50++;

                    absoluteAverage += number;
                }

                //Calculate average and print both
                MCViewModel.Log("Number Generator Tests", "------------");
                MCViewModel.Log("Number Generator Tests", $"Amount of items at .5 : {amountOfNo50}");
                MCViewModel.Log("Number Generator Tests", $"Average : {Math.Round(absoluteAverage / amountOfNumbers, 2)}");
            }
        }
    }
}
