using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.MonteCarlo
{
    public enum Club
    {
        Ajx,
        Fey,
        PSV,
        FCU,
        WII
    }

    public enum SoccerResult
    {
        Loss = 0,
        Draw = 1,
        Win = 2,
    }

    public static class SoccerResultHelper
    {
        public static int ToPoints(SoccerResult result, bool flip)
        {
            switch (result)
            {
                case SoccerResult.Loss:
                    return flip ? 3 : 0;
                case SoccerResult.Draw:
                    return 1;
                case SoccerResult.Win:
                    return flip ? 0 : 3;
            }
            return -1;
        }
    }

    public class ClubCombination
    {
        public Club Home { get; set; }
        public Club Away { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Loss { get; set; }

        public ClubCombination(Club home, Club away, int win, int draw, int loss)
        {
            Home = home;
            Away = away;
            Win = win;
            Draw = draw;
            Loss = loss;
        }
    }

    internal class ClubGoalOdds
    {
        public Club Club { get; set; }
        public double HS { get; set; }
        public double HC { get; set; }
        public double AS { get; set; }
        public double AC { get; set; }
    }

    internal class PoulesDataCollection
    {
        public List<ClubCombination> GetAllCombinations()
        {
            List<ClubCombination> all = new List<ClubCombination>();

            all.Add(new(Club.Ajx, Club.Fey, 65, 17, 18));
            all.Add(new(Club.Ajx, Club.PSV, 54, 21, 25));
            all.Add(new(Club.Ajx, Club.FCU, 74, 14, 12));
            all.Add(new(Club.Ajx, Club.WII, 78, 13, 9));

            all.Add(new(Club.Fey, Club.Ajx, 30, 21, 49));
            all.Add(new(Club.Fey, Club.PSV, 37, 24, 39));
            all.Add(new(Club.Fey, Club.FCU, 51, 22, 27));
            all.Add(new(Club.Fey, Club.WII, 60, 21, 19));

            all.Add(new(Club.PSV, Club.Ajx, 39, 22, 39));
            all.Add(new(Club.PSV, Club.Fey, 54, 22, 24));
            all.Add(new(Club.PSV, Club.FCU, 62, 20, 18));
            all.Add(new(Club.PSV, Club.WII, 62, 22, 16));

            all.Add(new(Club.FCU, Club.Ajx, 25, 14, 61));
            all.Add(new(Club.FCU, Club.Fey, 37, 23, 40));
            all.Add(new(Club.FCU, Club.PSV, 29, 24, 47));
            all.Add(new(Club.FCU, Club.WII, 52, 23, 25));

            all.Add(new(Club.WII, Club.Ajx, 17, 18, 65));
            all.Add(new(Club.WII, Club.Fey, 20, 26, 54));
            all.Add(new(Club.WII, Club.PSV, 23, 24, 53));
            all.Add(new(Club.WII, Club.FCU, 37, 25, 38));

            return all;
        }

        public List<ClubGoalOdds> AllGoalOdds()
        {
            List<ClubGoalOdds> all = new();

            all.Add(new() { Club = Club.Ajx, HS = 3.2, HC = 0.9, AS = 3.1, AC = 0.6 });
            all.Add(new() { Club = Club.Fey, HS = 2.4, HC = 1.1, AS = 2.2, AC = 0.8 });
            all.Add(new() { Club = Club.PSV, HS = 2.1, HC = 0.7, AS = 1.8, AC = 1.3 });
            all.Add(new() { Club = Club.FCU, HS = 1.9, HC = 1.2, AS = 3.0, AC = 2.4 });
            all.Add(new() { Club = Club.WII, HS = 1.4, HC = 1.7, AS = 1.0, AC = 1.5 });

            return all;
        }
    }
}

