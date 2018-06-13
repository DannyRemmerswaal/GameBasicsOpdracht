using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBasicsOpdracht.MVC.Model
{
    public class Wedstrijd
    {
        public Team ThuisTeam { get; set; }
        public Team UitTeam { get; set; }
        public int ThuisGoals { get; set; }
        public int UitGoals { get; set; }
        public int DoelpuntenVerschilThuis { get; set; }
        public int DoelpuntenVerschilUit { get; set; }

        public Wedstrijd(Team thuisteam, Team uitteam)
        {
            Controller.WedstrijdController wedstrijdcontroller = new Controller.WedstrijdController();
            ThuisTeam = thuisteam;
            UitTeam = uitteam;

            // Wedstrijd simuleren
            Tuple<int, int> Result = wedstrijdcontroller.WedstrijdSimulatie(this);
            ThuisGoals = Result.Item1;
            UitGoals = Result.Item2;
        }
    }
}
