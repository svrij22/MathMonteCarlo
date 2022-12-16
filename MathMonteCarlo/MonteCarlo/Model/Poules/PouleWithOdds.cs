using MathMonteCarlo.Numbers;
using MathMonteCarlo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.MonteCarlo.Model
{
    internal class PouleWithOdds : Poule
    {

        // Play poule                                        Delegate method for generating points
        public override void Play()
        {

            //Returns a list of all club combinations and W/D/L Ratios.
            List<ClubCombination> allCombinations = new PoulesDataCollection().GetAllCombinations();

            //'Play' the games
            foreach (var combi in allCombinations)
            {
                //Get the result with a lambda method
                var result = WeighedRandom.GetEnum<SoccerResult>(new[] { (double)combi.Loss,
                                                                (double)combi.Draw,
                                                                (double)combi.Win});

                //Add result to both clubs
                PointResults[combi.Home] += SoccerResultHelper.ToPoints(result, false);
                PointResults[combi.Away] += SoccerResultHelper.ToPoints(result, true);
            }
        }
    }
}
