using MathMonteCarlo.Numbers;
using MathMonteCarlo.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MathMonteCarlo.MonteCarlo.Opdracht1;

namespace MathMonteCarlo.MonteCarlo
{
    internal class Opdracht2
    {
        public static void Run(string time)
        {
            //Get all ratios
            List<ClubCombination> allRatios = new PoulesDataCollection().GetAllCombinations();

            //Create a global list of results
            Dictionary<Club, List<Position>> ClubResults = new();
            for (int i = 0; i < 5; i++) // Initialize
                ClubResults.Add((Club)i, new());


            //Run 100 simulations
            int simulations = 100;
            MCViewModel.Log($"Individual Game Results ({time})", $"Running 100 game simulations");
            for (int t = 0; t < simulations; t++)
            {

                //Create a temporary list of results
                Dictionary<Club, int> pointResults = new();
                for (int i = 0; i < 5; i++) // Initialize
                    pointResults.Add((Club)i, 0);

                // 'Play' the games
                foreach (var ratio in allRatios)
                {
                    //Use our weighed randomizer to draw out a result
                    var result = Weighed.WeighedRandom<SoccerResult>(new[] { ratio.Loss,
                                                                ratio.Draw,
                                                                ratio.Win});

                    //Add result to both clubs
                    pointResults[ratio.Home] += (int)result;
                    //win = 0, draw = 1, loss = 3, for opposing team
                    if (result != SoccerResult.Win)
                        pointResults[ratio.Away] += (result == SoccerResult.Loss) ? 3 : 1;
                }

                //Test
                MCViewModel.Log($"Individual Game Results ({time})", $"-------------");
                foreach (var club in pointResults.OrderBy(b => -b.Value))
                    MCViewModel.Log($"Individual Game Results ({time})", $"{club.Key} has {club.Value} points.");

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
            MCViewModel.Log($"Total Game Results ({time})", $"-------------");
            foreach (var club in ClubResults)
                MCViewModel.Log($"Total Game Results ({time})", $"{club.Key} : " +
                    $" 1st - {GetPerc(club.Value, Position.First)} % " +
                    $" 2nd - {GetPerc(club.Value, Position.Second)} % " +
                    $" 3rd - {GetPerc(club.Value, Position.Third)} % " +
                    $" 4rd - {GetPerc(club.Value, Position.Fourth)} % " +
                    $" 5th - {GetPerc(club.Value, Position.Fifth)} % ");
        }
    }
}
