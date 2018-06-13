using GameBasicsOpdracht.MVC.Model;
using System;

namespace GameBasicsOpdracht.MVC.Controller
{
    public class WedstrijdController
    {
        public Tuple<int, int> WedstrijdSimulatie(Model.Wedstrijd wedstrijd)
        {
            int ThuisRating = BerekeningController.BerekenTeamRating(wedstrijd.ThuisTeam);
            int UitRating = BerekeningController.BerekenTeamRating(wedstrijd.UitTeam);
            Random r = new Random();
            int ThuisGoals = 0;
            int UitGoals = 0;

            //Bereken Team 
            wedstrijd.ThuisTeam.Scoorkans = wedstrijd.ThuisTeam.TeamSterkte / 2;
            wedstrijd.UitTeam.Scoorkans = wedstrijd.ThuisTeam.TeamSterkte / 2;

            //Bereken rating
            BerekeningController.BerekenScoorkans(wedstrijd);

            //Wedstrijdsimulatie
            for (int i = 0; i < 2; i++)  
            {
                Team tempteam = new Team();
                if (i == 0)
                    tempteam = wedstrijd.ThuisTeam;
                else
                    tempteam = wedstrijd.UitTeam;
                for (int x = 0; x < 91; x++)   
                {
                    int a = r.Next(0, 5000);
                    if (a < tempteam.Scoorkans + 50)
                    {
                        tempteam.TotaalGoalsVoor += 1;
                        if (i == 0)
                            ThuisGoals += 1;
                        else
                            UitGoals += 1;
                    }
                }
            }
            // Doelsaldo berekenen
            wedstrijd.ThuisTeam.DoelSaldo += ThuisGoals - UitGoals;
            wedstrijd.UitTeam.DoelSaldo += UitGoals - ThuisGoals;
            wedstrijd.ThuisTeam.TotaalGoalsTegen += UitGoals;
            wedstrijd.UitTeam.TotaalGoalsTegen += ThuisGoals;

            // Terugkoppeling resultaat
            Tuple<int, int> MatchResult = Tuple.Create(ThuisGoals, UitGoals);
            BerekeningController.BerekenPunten(MatchResult, wedstrijd);
            return MatchResult;
        }

    }
}

