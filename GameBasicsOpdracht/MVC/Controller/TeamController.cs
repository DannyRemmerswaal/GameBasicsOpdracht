using System;
using GameBasicsOpdracht.MVC.Model;

namespace GameBasicsOpdracht.MVC.Controller
{
    public class TeamController
    {
        public static void VerdeelSpelers(Poule poule)
        {
            for (int i = 0; i < poule.Spelers.Count; i++)
            {
                Speler speler = poule.Spelers[i];
                foreach (Team team in poule.Teams)
                {
                    // Speler bij team zoeken
                    if (speler.Team == team.TeamNaam)
                    {
                        // Speler toevoegen aan team
                        team.BasisSpelers.Add(speler);
                        Console.WriteLine("Added: " + speler.Naam + " To: " + team.TeamNaam + " With Rating: " + speler.Rating);
                    }
                }
            }
        }
    }
}

