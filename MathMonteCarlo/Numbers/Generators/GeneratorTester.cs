using MathMonteCarlo.Numbers.Generators;
using MathMonteCarlo.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MathMonteCarlo.Numbers
{
    /// <summary>
    /// Tests the generator
    /// </summary>

    internal class GeneratorTester
    {

        public static void TestCongruentual()
        {
            Test<CongruentualNumberGenerator>(new CongruentualNumberGenerator());
        }
        public static void TestMidSquare()
        {
            Test<MidSquareNumberGenerator>(new MidSquareNumberGenerator());
        }
        public static void TestBuiltIn()
        {
            Test<BuiltInRandomNumberGenerator>(new BuiltInRandomNumberGenerator());
        }
        public static void TestLagFib()
        {
            Test<LagFibNumberGenerator>(new LagFibNumberGenerator());
        }
        public static void TestMersTwist()
        {
            Test<MersenneTwisterNumberGenerator>(new MersenneTwisterNumberGenerator());
        }

        public static void Test<T>(NumberGenerator numberGenerator)
        {
            MCViewModel.Log($"Number Generator Tests T={typeof(T)}", "Running tests for class type " + typeof(T));
            Stopwatch stopwatch = new();
            stopwatch.Start();

            // Number of tests to run
            for (int i = 0; i < 10; i++)
            {

                //results
                List<double> results = new();

                //Generate
                int amountOfNumbers = 2000;
                for (int j = 0; j < amountOfNumbers; j++)
                {
                    var number = numberGenerator.NextDouble();
                    results.Add(number);
                }

                // Amount of numbers under 50%
                int amountOfNo50 = results.Count(no => no <= .5);
                int amountOfNo001 = results.Count(no => no <= .01);

                //Calculate average and print both
                MCViewModel.Log($"Number Generator Tests T={typeof(T)}", "------------");
                MCViewModel.Log($"Number Generator Tests T={typeof(T)}", $"Numbers generated : {amountOfNumbers}");
                MCViewModel.Log($"Number Generator Tests T={typeof(T)}", $"Amount of items under 0.01 : {amountOfNo001}");
                MCViewModel.Log($"Number Generator Tests T={typeof(T)}", $"Amount of items at .5 : {amountOfNo50}");

                MCViewModel.Log($"Number Generator Tests T={typeof(T)}", $"Average : {Average(results)}");
                MCViewModel.Log($"Number Generator Tests T={typeof(T)}", $"Freq : {Frequency(results)}");

                MCViewModel.Log($"Number Generator Tests T={typeof(T)}", $"Variance : {Math.Round(Variance(results), 2)}");
                MCViewModel.Log($"Number Generator Tests T={typeof(T)}", $"StdDev : {Math.Round(StdDev(results), 2)}");
            }

            stopwatch.Stop();
            MCViewModel.Log($"Number Generator Tests T={typeof(T)}", "------------");
            MCViewModel.Log($"Number Generator Tests T={typeof(T)}", $"Generating numbers took {stopwatch.Elapsed.TotalMilliseconds} milliseconds.");
        }


        /// <summary>
        /// TODO, Freq test aanpassen -> gebruik seed ipv ushort gebaseerd op double.
        /// Nieuw item in abstracte klasse
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static double Frequency(List<double> values)
        {
            var freq = 0.0d;
            foreach (double value in values)
            {
                ushort _use = (ushort)Math.Round(value * 65535);

                var bytes = BitConverter.GetBytes(_use);
                var bits = new BitArray(bytes);

                foreach (bool bit in bits)
                    freq += bit ? 1 : -1;
            }
            return freq;
        }

        private static double Average(List<double> values)
        {
            return Math.Round(values.Sum() / values.Count(), 2);
        }

        public static double Variance(List<double> values)
        {
            if (values.Count > 1)
            {
                double avg = Average(values);
                double variance = 0.0;
                
                foreach (double value in values)
                    variance += Math.Pow(value - avg, 2.0);

                return variance / values.Count;
            }
            return 0.0;
        }

        private static double StdDev(List<double> values)
        {
            return Math.Sqrt(Variance(values));
        }
    }
}
