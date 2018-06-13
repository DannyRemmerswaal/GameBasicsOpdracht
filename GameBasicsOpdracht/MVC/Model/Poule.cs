using System.Collections.Generic;


namespace GameBasicsOpdracht.MVC.Model
{
    public class Poule
    {
        public List<Speler> Spelers { get; set; }       
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<Wedstrijd> Wedstrijden { get; set; } = new List<Wedstrijd>();
        public void CreateTeams()
        {
            // Add all teams to the list
            Teams.Add(new Team {TeamNaam = "Nederland" });
            Teams.Add(new Team {TeamNaam = "Italië"});
            Teams.Add(new Team {TeamNaam = "Verenigde Staten" });
            Teams.Add(new Team {TeamNaam = "Ecuador"});
        }
    }
}
