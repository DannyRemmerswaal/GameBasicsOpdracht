using GameBasicsOpdracht.MVC.View;
using System.Windows.Forms;

namespace GameBasicsOpdracht
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainView());
        }
    }
}
