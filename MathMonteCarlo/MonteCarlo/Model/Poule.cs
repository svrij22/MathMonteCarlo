using MathMonteCarlo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.MonteCarlo.Model
{
    internal class Poule
    {

        //Create a temporary list of results for Points
        Dictionary<Club, int> pointResults = new();

        public Poule()
        {
            //Initialize list.
            for (int i = 0; i < 5; i++)
                pointResults.Add((Club)i, 0);
        }

        // Play poule                                        Delegate method for generating points
        public void Play(Func<ClubCombination, SoccerResult> calculatePointsDelegate)
        {

            //Returns a list of all club combinations and W/D/L Ratios.
            List<ClubCombination> allCombinations = new PoulesDataCollection().GetAllCombinations();

            //'Play' the games
            foreach (var combi in allCombinations)
            {
                //Get the result with a lambda method
                var result = calculatePointsDelegate(combi);

                //Add result to both clubs
                pointResults[combi.Home] += SoccerResultHelper.ToPoints(result, false);
                pointResults[combi.Away] += SoccerResultHelper.ToPoints(result, true);
            }
        }

        public List<Club> OrderedClubResults()
        {
            //Order by points
            var ordered = pointResults
                            .OrderBy(b => -b.Value) // order by value = amount of points.
                            .Select(kv => kv.Key) // get the keys = club names.
                            .ToList(); // turn into list.
            return ordered;
        }

        /// <summary>
        /// Debugging
        /// </summary>
        public void PrintResults(int DEBUG_runIndex)
        {
            //Log block
            MCViewModel.Log($"Individual Game Results ({DEBUG_runIndex})", $"-------------");
            foreach (var club in pointResults.OrderBy(b => -b.Value))
                MCViewModel.Log($"Individual Game Results ({DEBUG_runIndex})", $"{club.Key} has {club.Value} points.");
        }
    }
}
