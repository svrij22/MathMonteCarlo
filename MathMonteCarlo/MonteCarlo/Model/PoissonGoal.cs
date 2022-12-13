using MathMonteCarlo.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.MonteCarlo.Model
{
    internal class PoissonGoal
    {
        public ClubCombination Combi { get; set; }
        public PoissonGoal(ClubCombination comb)
        {
            Combi = comb;
        }

        /// <summary>
        /// PoissonGoal class for calculating using the average goal odds and Poisson method.
        /// </summary>
        /// <returns></returns>
        public SoccerResult Calculate()
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

            //Use Poisson distrubution function to calculate random goal variables
            var GOALS_HOME = Poisson.GetPoisson(__homeAverage);
            var GOALS_OTHER = Poisson.GetPoisson(__otherAverage);

            //Return result
            if (GOALS_HOME == GOALS_OTHER)
                return SoccerResult.Draw;
            if (GOALS_HOME > GOALS_OTHER)
                return SoccerResult.Win;
            return SoccerResult.Loss;
        }
    }
}
