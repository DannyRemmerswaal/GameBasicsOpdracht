using System;
using System.Collections.Generic;

namespace GameBasicsOpdracht.MVC.Model
{
    public class Team
    {
        public String TeamNaam { get; set; }
        public double TeamSterkte { get; set; }
        public double Scoorkans { get; set; }
        public int TotaalGoalsVoor { get; set; } = 0;
        public int TotaalGoalsTegen { get; set; } = 0;
        public int DoelSaldo { get; set; }
        public int TotaalPunten { get; set; }
        public List<Speler> BasisSpelers { get; set; } = new List<Speler>();
        public List<Speler> WisselSpelers { get; set; } = new List<Speler>();       
    }
}
