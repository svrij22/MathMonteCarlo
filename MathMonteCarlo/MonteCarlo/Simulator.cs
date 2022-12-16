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


        public string DEBUG_name = "";
        public Simulator(string simName = "test")
        {
            //Set print name
            DEBUG_name = simName;
        }
        /// <summary>
        /// Run opdracht 1
        /// </summary>
        public void RunOpdracht1()
        {
            //Runs the simulation with as delegate the WeighedRandom function
            Run((combi) =>
                {
                    //Use our weighed randomizer to draw out a result
                    var enumValue = WeighedRandom.GetEnum<SoccerResult>(new[] { (double)combi.Loss,
                                                                (double)combi.Draw,
                                                                (double)combi.Win});
                    return enumValue;
                });
        }

        /// <summary>
        /// Run opdracht 2
        /// </summary>
        public void RunOpdracht2()
        {
            //Runs the simulation with as delegate the PoissonGoal Calculate() function
            Run((combi) =>
                {
                    //Use our PoissonGoal class and pass our combination to draw out a random result
                    return PoissonGoalExt.Calculate(combi);
                });
        }

        public List<Poule> PlayedPoules = new();

        /// <summary>
        /// Run simulation
        /// </summary>
        /// <param name="calculatePointsDelegate"></param>
        /// <param name="amountOfSimulations"></param>
        public void Run(Func<ClubCombination, SoccerResult> calculatePointsDelegate,
                        int amountOfSimulations = 250)
        {
            //Run X simulations
            for (int t = 0; t < amountOfSimulations; t++)
            {
                //Create poule and play
                Poule poule = new Poule();

                poule.Play(calculatePointsDelegate);

                PlayedPoules.Add(poule);
            }
        }

        //Creates a global list of club results used for calculating percentages.
        Dictionary<Club, List<Position>> ClubResults = new();

        /// <summary>
        /// Method to calculate statistics
        /// </summary>
        public void CalculateStatistics()
        {
            // Initialize list.
            for (int i = 0; i < 5; i++)
                ClubResults.Add((Club)i, new());

            //For each poule
            foreach (var poule in PlayedPoules)
            {

                //Get ordered results
                var orderedResults = poule.OrderedClubResults();

                //Add to results
                for (int i = 0; i < orderedResults.Count; i++)
                {
                    //Get our club
                    var club = orderedResults[i];

                    //Cast our index to club position e.g. 0 = First, 1 = Second.
                    ClubResults[club].Add((Position)i);
                }
            }
        }

        public void PrintStatistics(int amountOfSimulations = 250)
        {
            //Log
            MCViewModel.Log($"Individual Game Results ({DEBUG_name})", $"Ran {amountOfSimulations} game simulations");

            //Log individual results


            //lambda func to calculate percentage of certain position in list of position string
            // e.g. 23x times Position.First in List<Position> with 100 Positions will return 23%
            Func<List<Position>, Position, double> CalculatePercentageOfPositions = (positions, matchingPos) =>
            {
                float total = positions.Count;
                float matches = positions.Count(p => p == matchingPos);
                return Math.Round((matches / total) * 100f, 1);
            };

            //LOG
            MCViewModel.Log($"Total Game Results ({DEBUG_name})", $"-------------");
            foreach (var club in ClubResults)
                MCViewModel.Log($"Total Game Results ({DEBUG_name})", $"{club.Key} : " +
                    $" 1st - {CalculatePercentageOfPositions(club.Value, Position.First)} % " +
                    $" 2nd - {CalculatePercentageOfPositions(club.Value, Position.Second)} % " +
                    $" 3rd - {CalculatePercentageOfPositions(club.Value, Position.Third)} % " +
                    $" 4rd - {CalculatePercentageOfPositions(club.Value, Position.Fourth)} % " +
                    $" 5th - {CalculatePercentageOfPositions(club.Value, Position.Fifth)} % ");
        }
    }
}