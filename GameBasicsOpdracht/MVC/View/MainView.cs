using GameBasicsOpdracht.Json;
using GameBasicsOpdracht.MVC.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GameBasicsOpdracht.MVC.View
{
    public partial class MainView : Form
    {
        public Poule Current_Poule { get; set; } = new Poule();
        private Model.Team Team_A { get; set; }       // Team 1/4
        private Model.Team Team_B { get; set; }       // Team 2/4
        private Model.Team Team_C { get; set; }       // Team 3/4
        private Model.Team Team_D { get; set; }       // Team 4/4
        private List<Team> Poule_Teams;
        private int MatchesPlayed { get; set; } = 0;    // Gespeelde wedstrijden, 6 = groepsfase (bereken eindstand)

        public MainView()
        {
            InitializeComponent();
            // Json file uitlezen
            JsonReader reader = new JsonReader();
            Current_Poule.Spelers = reader.LeesSpelers();
            Current_Poule.Teams = new List<Team>();
            Current_Poule.CreateTeams();
            Controller.TeamController.VerdeelSpelers(Current_Poule);

            // Willekeurige wedstrijden
            int[] randomnumbers = new int[4];
            randomnumbers = Enumerable.Range(0, Current_Poule.Teams.Count).OrderBy(g => Guid.NewGuid()).Take(4).ToArray();
            Team_A = Current_Poule.Teams[randomnumbers[0]];
            Team_B = Current_Poule.Teams[randomnumbers[1]];
            Team_C = Current_Poule.Teams[randomnumbers[2]];
            Team_D = Current_Poule.Teams[randomnumbers[3]];

            foreach (Label label in Controls.OfType<Label>())
            {
                if (label.Tag.ToString().Contains("Team 1"))
                    label.Text = Team_A.TeamNaam;
                else if (label.Tag.ToString().Contains("Team 2"))
                    label.Text = Team_B.TeamNaam;
                else if (label.Tag.ToString().Contains("Team 3"))
                    label.Text = Team_C.TeamNaam;
                else if (label.Tag.ToString().Contains("Team 4"))
                    label.Text = Team_D.TeamNaam;
            }
            // Teamrating berekenen
            for (int i = 0; i < Current_Poule.Teams.Count; i++)
            {
                Current_Poule.Teams[i].TeamSterkte = Controller.BerekeningController.BerekenTeamRating(Current_Poule.Teams[i]);
            }

            Poule_Teams = new List<Team>() { Team_A, Team_B, Team_C, Team_D };
        }

        private void Set_Score(int Thuisgoals, int Uitgoals, Object buttontag)
        {
            foreach (Label label in Controls.OfType<Label>())
            {
                if (label.Tag != null)
                {
                    // Check which match the score is from
                    if (label.Tag.ToString().Contains(buttontag.ToString()[4]) && label.Tag.ToString().Contains(buttontag.ToString()[5]))
                    {
                        // Check if the label is Home or Out
                        if (label.Tag.ToString()[2] == 'H')
                        {
                            label.Text = Thuisgoals.ToString();
                            label.Visible = true;
                        }
                        else
                        {
                            label.Text = Uitgoals.ToString();
                            label.Visible = true;
                        }
                    }
                }
            }
        }

        private void Check_PouleRank()
        {
            // Sort rank on: 1. Total points 2. Goaldifference  3. Goals For
            Poule_Teams = Poule_Teams.OrderByDescending(x => x.TotaalPunten).ThenByDescending(y => y.DoelSaldo).ThenBy(z => z.TotaalGoalsVoor).ToList();

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    lb_land1.Text = "Land: " + Poule_Teams[i].TeamNaam;
                    lb_punten1.Text = "Punten: " + Poule_Teams[i].TotaalPunten;  
                    lb_doelsaldo1.Text = "Doelsaldo: " + Poule_Teams[i].DoelSaldo;   
                    lb_goalsvoor1.Text = "Doelpunten voor: " + Poule_Teams[i].TotaalGoalsVoor;       
                    lb_goalstegen1.Text = "Doelpunten tegen: " + Poule_Teams[i].TotaalGoalsTegen;       
                }
                if (i == 1)
                {
                    lb_land2.Text = "Land: " + Poule_Teams[i].TeamNaam;
                    lb_punten2.Text = "Punten: " + Poule_Teams[i].TotaalPunten;
                    lb_doelsaldo2.Text = "Doelsaldo: " + Poule_Teams[i].DoelSaldo;
                    lb_goalsvoor2.Text = "Doelpunten voor: " + Poule_Teams[i].TotaalGoalsVoor;
                    lb_goalstegen2.Text = "Doelpunten tegen: " + Poule_Teams[i].TotaalGoalsTegen;
                }
                if (i == 2)
                {
                    lb_land3.Text = "Land: " + Poule_Teams[i].TeamNaam;
                    lb_punten3.Text = "Punten: " + Poule_Teams[i].TotaalPunten;
                    lb_doelsaldo3.Text = "Doelsaldo: " + Poule_Teams[i].DoelSaldo;
                    lb_goalsvoor3.Text = "Doelpunten voor: " + Poule_Teams[i].TotaalGoalsVoor;
                    lb_goalstegen3.Text = "Doelpunten tegen: " + Poule_Teams[i].TotaalGoalsTegen;
                }
                if (i == 3)
                {
                    lb_land4.Text = "Land: " + Poule_Teams[i].TeamNaam;
                    lb_punten4.Text = "Punten: " + Poule_Teams[i].TotaalPunten;
                    lb_doelsaldo4.Text = "Doelsaldo: " + Poule_Teams[i].DoelSaldo;
                    lb_goalsvoor4.Text = "Doelpunten voor: " + Poule_Teams[i].TotaalGoalsVoor;
                    lb_goalstegen4.Text = "Doelpunten tegen: " + Poule_Teams[i].TotaalGoalsTegen;
                }
            }

            foreach (Control element in Controls)
            { 
                if (element.Tag != null)
                {
                    if (element.Tag.ToString().Contains("Results"))
                        element.Visible = true;
                }
            }
        }

        private void SimulatieButton_Click(object sender, EventArgs e)
        {
            Team teamHome = new Team();
            Team teamOut = new Team();
            var ButtonTag = ((Button)sender).Tag;
            ((Button)sender).Visible = false;
            MatchesPlayed++;

            // Check the Home team
            if (ButtonTag.ToString()[2] == 'A')
                teamHome = Team_A;
            else if (ButtonTag.ToString()[2] == 'B')
                teamHome = Team_B;
            else if (ButtonTag.ToString()[2] == 'C')
                teamHome = Team_C;
            else
                teamHome = Team_D;

            // Check the Out team
            if (ButtonTag.ToString()[3] == 'A')
                teamOut = Team_A;
            else if (ButtonTag.ToString()[3] == 'B')
                teamOut = Team_B;
            else if (ButtonTag.ToString()[3] == 'C')
                teamOut = Team_C;
            else
                teamOut = Team_D;

            // Start a match, write score to view, and check how many matches are played
            Wedstrijd wedstrijd = new Wedstrijd(teamHome, teamOut);
            // Add match to list of played matches
            Current_Poule.Wedstrijden.Add(wedstrijd);
            // Write score to labels
            Set_Score(wedstrijd.ThuisGoals, wedstrijd.UitGoals, ButtonTag);
            // Check amount of played matches
            if (MatchesPlayed == 6)
                Check_PouleRank();
        }


        private void Herstart_Click(object sender, EventArgs e)
        {
            Close();
            View.MainView mainView = new View.MainView();
            mainView.Show();
        }
    }
}
