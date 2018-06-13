using GameBasicsOpdracht.MVC.Model;
using System;


namespace GameBasicsOpdracht.MVC.Controller
{
    public class BerekeningController
    {
        
        public static int BerekenTeamRating(Model.Team team)
        {
            int Rating = 0;
            foreach (Speler speler in team.BasisSpelers)
            {
                Rating += speler.Rating;
            }
            int teamRating = Convert.ToInt32(Rating / 11);
            return teamRating;
        }
 
        public static void BerekenScoorkans(Wedstrijd wedstrijd)
        {
            for (int x = 0; x < 2; x++)
            {
                Team tempTeam = new Team();
                if (x == 0)
                    tempTeam = wedstrijd.ThuisTeam;
                else
                    tempTeam = wedstrijd.UitTeam;
                for (int y = 0; y < tempTeam.BasisSpelers.Count; y++)
                {
                    Speler speler = tempTeam.BasisSpelers[y];
                    double scoorkans = speler.Rating;
                    Console.WriteLine(speler.Naam + scoorkans);
                    if (speler.Positie == "DM" || speler.Positie == "V" || speler.Positie == "VM") //Scoringskans verkleint wanneer speler verdedigende taken heeft
                    {
                        if (x == 0)
                            wedstrijd.UitTeam.Scoorkans -= scoorkans;
                        else
                            wedstrijd.ThuisTeam.Scoorkans -= scoorkans;
                    }
                    else // Scoringskans vergroot wanneer speler aanvallende taken heeft
                    {
                        if (x == 0)
                            wedstrijd.ThuisTeam.Scoorkans += scoorkans;
                        else
                            wedstrijd.UitTeam.Scoorkans += scoorkans;
                    }
               
                }
                
            }
        }
        public static void BerekenPunten(Tuple<int, int> Score, Wedstrijd wedstrijd)
        {
            if (Score.Item1 == Score.Item2)
            {
                wedstrijd.ThuisTeam.TotaalPunten += 1;
                wedstrijd.UitTeam.TotaalPunten += 1;
            }
            else if (Score.Item1 > Score.Item2)
            {
                wedstrijd.ThuisTeam.TotaalPunten += 3;
            }
          
            else if (Score.Item1 < Score.Item2)
            {
                wedstrijd.UitTeam.TotaalPunten += 3;
            }
           
  
        }

    }
}

