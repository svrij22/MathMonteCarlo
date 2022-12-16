using MathMonteCarlo.Numbers;
using MathMonteCarlo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.MonteCarlo.Model
{
    internal class PouleWithPoisson : Poule
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
                var result = GetPoissonGoalsResults(combi);

                var goalsHome = result[0];
                var goalsOther = result[1];

                //Return result
                var gameResult = SoccerResult.Loss;
                if (goalsHome == goalsOther)
                    gameResult = SoccerResult.Draw;
                if (goalsHome > goalsOther)
                    gameResult = SoccerResult.Win;

                //Add goals
                TotalGoals[combi.Home] += goalsHome;
                TotalGoals[combi.Home] += goalsOther;


                //Add result to both clubs
                PointResults[combi.Home] += SoccerResultHelper.ToPoints(gameResult, false);
                PointResults[combi.Away] += SoccerResultHelper.ToPoints(gameResult, true);
            }
        }
        public List<int> GetPoissonGoalsResults(ClubCombination Combi)
        {

            //Get all our odds
            List<ClubGoalOdds> allGoalOdds = new PoulesDataCollection()
                                                .AllGoalOdds();
            //Get goal odds for HOME and AWAY
            var goalOddsHome = allGoalOdds
                    .First(go => go.Club == Combi.Home);
            var goalOddsAway = allGoalOdds
                    .First(go => go.Club == Combi.Away);


            //om een indicatie te krijgen van het gemiddelde aantal doelpunten dat door Ajax thuis tegen PSV wordt gescoord:
            //(HS(Ajax) + AC(PSV)) / 2-- > (3.2 + 1.3) / 2 = 2.25.

            //Calculate home score average
            var __homeAverage = (goalOddsHome.HS + goalOddsAway.AC) / 2;

            //Calculate home score average
            var __otherAverage = (goalOddsAway.AS + goalOddsHome.HC) / 2;

            //Use Poisson distribution function to calculate random goal variables
            var GOALS_HOME = PoissonMath.GetPoissionCustom(__homeAverage);
            var GOALS_OTHER = PoissonMath.GetPoissionCustom(__otherAverage);

            return new() { GOALS_HOME, GOALS_OTHER };
        }
    }
}
