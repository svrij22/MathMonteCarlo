using MathMonteCarlo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.MonteCarlo.Model
{
    public abstract class Poule
    {

        //Create a temporary list of results for Points
        protected Dictionary<Club, int> PointResults = new();
        protected Dictionary<Club, int> TotalGoals = new();

        public Poule()
        {
            //Initialize lists.
            for (int i = 0; i < 5; i++)
                PointResults.Add((Club)i, 0);
            for (int i = 0; i < 5; i++)
                TotalGoals.Add((Club)i, 0);
        }

        // Play method
        public abstract void Play();

        public List<Club> OrderedClubResults()
        {
            //WEIGHS = (100 * points) + goals for more difference between shared places

            //Order by points
            var ordered = PointResults
                            .OrderBy(b => -((b.Value * 100) + TotalGoals[b.Key])) // order by value = amount of points * 100 + amt of goals.
                            .Select(kv => kv.Key) // get the keys = club names.
                            .ToList(); // turn into list.
            return ordered;
        }

        /// <summary>
        /// Debugging
        /// </summary>
        public void PrintResults(string DEBUG_name)
        {
            //Log block
            MCViewModel.Log($"Individual Game Results ({DEBUG_name})", $"-------------");
            foreach (var club in PointResults.OrderBy(b => -b.Value))
                MCViewModel.Log($"Individual Game Results ({DEBUG_name})", $"{club.Key} has {club.Value} points and made {TotalGoals[club.Key]} goals.");
        }
    }
}
