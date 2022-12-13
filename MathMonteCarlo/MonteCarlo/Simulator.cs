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
            //Runs the simulation with as delegate the WeighedRandom function
            RunSimulation(logName,
                (combi) =>
                {
                    //Use our weighed randomizer to draw out a result
                    var enumValue = WeighedRandom.GetEnum<SoccerResult>(new[] { combi.Loss,
                                                                combi.Draw,
                                                                combi.Win});
                    return enumValue;
                });
        }

        /// <summary>
        /// Run opdracht 2
        /// </summary>
        /// <param name="runIndex"></param>
        public static void RunOpdracht2(string runIndex)
        {
            //Runs the simulation with as delegate the PoissonGoal Calculate() function
            RunSimulation(runIndex,
                (combi) =>
                {
                    //Use our PoissonGoal class and pass our combination to draw out a random result
                    return new PoissonGoal(combi).Calculate();
                });
        }

        public static void RunSimulation(string runIndex,
                                         Func<ClubCombination, SoccerResult> calculatePointsDelegate,
                                         int amountOfSimulations = 250)
        {
            //Returns a list of all club combinations and W/D/L Ratios.
            List<ClubCombination> allCombinations = new PoulesDataCollection().GetAllCombinations();

            //Creates a global list of club results used for calculating percentages.
            Dictionary<Club, List<Position>> ClubResults = new();
            
            // Initialize list.
            for (int i = 0; i < 5; i++) 
                ClubResults.Add((Club)i, new());


            //Log
            MCViewModel.Log($"Individual Game Results ({runIndex})", $"Running {amountOfSimulations} game simulations");

            //Run X simulations
            for (int t = 0; t < amountOfSimulations; t++)
            {

                //Create a temporary list of results for Points
                Dictionary<Club, int> pointResults = new();

                //Initialize list.
                for (int i = 0; i < 5; i++) 
                    pointResults.Add((Club)i, 0);

                //'Play' the games
                foreach (var combi in allCombinations)
                {
                    //Get the result with a lambda method
                    var result = calculatePointsDelegate(combi);

                    //Add result to both clubs
                    pointResults[combi.Home] += SoccerResultHelper.ToPoints(result, false);
                    pointResults[combi.Away] += SoccerResultHelper.ToPoints(result, true);
                }

                //Log block
                MCViewModel.Log($"Individual Game Results ({runIndex})", $"-------------");
                foreach (var club in pointResults.OrderBy(b => -b.Value))
                    MCViewModel.Log($"Individual Game Results ({runIndex})", $"{club.Key} has {club.Value} points.");
                
                //Order by points
                var ordered = pointResults
                                .OrderBy(b => -b.Value) // order by value = amount of points.
                                .Select(kv => kv.Key) // get the keys = club names.
                                .ToList(); // turn into list.

                //Add to results
                for (int i = 0; i < ordered.Count; i++)
                {
                    var club = ordered[i]; //Get our club
                    //Cast our index to club position e.g. 0 = First, 1 = Second.
                    ClubResults[club].Add((Position)i);
                }
            }

            //lambda func to calculate percentage of certain position in list of position string
            // e.g. 23x times Position.First in List<Position> with 100 Positions will return 23%
            Func<List<Position>, Position, double> CalculatePercentageOfPositions = (positions, matchingPos) =>
            {
                float total = positions.Count;
                float matches = positions.Count(p => p == matchingPos);
                return Math.Round((matches / total) * 100f, 1);
            };

            //LOG
            MCViewModel.Log($"Total Game Results ({runIndex})", $"-------------");
            foreach (var club in ClubResults)
                MCViewModel.Log($"Total Game Results ({runIndex})", $"{club.Key} : " +
                    $" 1st - {CalculatePercentageOfPositions(club.Value, Position.First)} % " +
                    $" 2nd - {CalculatePercentageOfPositions(club.Value, Position.Second)} % " +
                    $" 3rd - {CalculatePercentageOfPositions(club.Value, Position.Third)} % " +
                    $" 4rd - {CalculatePercentageOfPositions(club.Value, Position.Fourth)} % " +
                    $" 5th - {CalculatePercentageOfPositions(club.Value, Position.Fifth)} % ");
        }
    }
}