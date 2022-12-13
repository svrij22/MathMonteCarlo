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

        public SoccerResult Calculate()
        {

            //Get all our odds
            List<ClubGoalOdds> allGoalOdds = new PoulesDataCollection()
                                                .AllGoalOdds();

            //om een indicatie te krijgen van het gemiddelde aantal doelpunten dat door Ajax thuis tegen PSV wordt gescoord:
            //(HS(Ajax) + AC(PSV)) / 2-- > (3.2 + 1.3) / 2 = 2.25.
            //Get average home score == HOME GOALS
            var homeScores = allGoalOdds
                .First(go => go.Club == Combi.Home).HS;

            //Get average home conceded == OTHER CONCEDES
            var otherAwayConceded = allGoalOdds
                .First(go => go.Club == Combi.Away).AC;

            //Calculate home score average
            var __homeAverage = (homeScores + otherAwayConceded) / 2;

            //Get average away score == OTHER GOALS
            var awayScores = allGoalOdds
                .First(go => go.Club == Combi.Away).AS;

            //Get average away conceded == HOME CONCEDES
            var homeConceded = allGoalOdds
                .First(go => go.Club == Combi.Home).HC;

            //Calculate home score average
            var __otherAverage = (awayScores + homeConceded) / 2;

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
