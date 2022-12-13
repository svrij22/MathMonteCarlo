using MathMonteCarlo.MonteCarlo.Model;
using MathMonteCarlo.Numbers;
using MathMonteCarlo.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.MonteCarlo
{
    public enum Position
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth
    }
    internal class Simulator
    {

        /// <summary>
        /// Run opdracht 1
        /// </summary>
        /// <param name="logName"></param>
        public static void RunOpdracht1(string logName)
        {
            RunSimulation(logName,
                (combi) =>
                {

                    return Weighed.WeighedRandom<SoccerResult>(new[] { combi.Loss,
                                                                combi.Draw,
                                                                combi.Win});
                });
        }

        /// <summary>
        /// Run opdracht 2
        /// </summary>
        /// <param name="runIndex"></param>
        public static void RunOpdracht2(string runIndex)
        {
            RunSimulation(runIndex,
                (combi) =>
                {

                    return new PoissonGoal(combi).Calculate();
                });
        }

        public static void RunSimulation(string runIndex,
            Func<ClubCombination, SoccerResult> lambdaGetResult,
            int amountOfSimulations = 250)
        {
            //Get all ratios
            List<ClubCombination> allRatios = new PoulesDataCollection().GetAllCombinations();

            //Create a global list of results
            Dictionary<Club, List<Position>> ClubResults = new();
            for (int i = 0; i < 5; i++) // Initialize
                ClubResults.Add((Club)i, new());


            //Run 100 simulations
            MCViewModel.Log($"Individual Game Results ({runIndex})", $"Running {amountOfSimulations} game simulations");
            for (int t = 0; t < amountOfSimulations; t++)
            {

                //Create a temporary list of results
                Dictionary<Club, int> pointResults = new();
                for (int i = 0; i < 5; i++) // Initialize
                    pointResults.Add((Club)i, 0);

                // 'Play' the games
                foreach (var combi in allRatios)
                {
                    //Use our weighed randomizer to draw out a result
                    var result = lambdaGetResult(combi);

                    //Add result to both clubs
                    pointResults[combi.Home] += (int)result;
                    //win = 0, draw = 1, loss = 3, for opposing team
                    if (result != SoccerResult.Win)
                        pointResults[combi.Away] += (result == SoccerResult.Loss) ? 3 : 1;
                }

                //Test
                MCViewModel.Log($"Individual Game Results ({runIndex})", $"-------------");
                foreach (var club in pointResults.OrderBy(b => -b.Value))
                    MCViewModel.Log($"Individual Game Results ({runIndex})", $"{club.Key} has {club.Value} points.");

                //Order
                var ordered = pointResults
                                .OrderBy(b => -b.Value)
                                .Select(kv => kv.Key)
                                .ToList();

                //Add to results
                for (int i = 0; i < ordered.Count; i++)
                {
                    var club = ordered[i];
                    ClubResults[club].Add((Position)i);
                }
            }

            //lambda func to calc perc string
            Func<List<Position>, Position, double> GetPerc = (positions, matchingPos) =>
            {
                float total = positions.Count;
                float matches = positions.Count(p => p == matchingPos);
                return Math.Round((matches / total) * 100f, 1);
            };

            //Test
            MCViewModel.Log($"Total Game Results ({runIndex})", $"-------------");
            foreach (var club in ClubResults)
                MCViewModel.Log($"Total Game Results ({runIndex})", $"{club.Key} : " +
                    $" 1st - {GetPerc(club.Value, Position.First)} % " +
                    $" 2nd - {GetPerc(club.Value, Position.Second)} % " +
                    $" 3rd - {GetPerc(club.Value, Position.Third)} % " +
                    $" 4rd - {GetPerc(club.Value, Position.Fourth)} % " +
                    $" 5th - {GetPerc(club.Value, Position.Fifth)} % ");
        }
    }
}
