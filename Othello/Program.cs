using System;
using System.Windows.Forms;

namespace Ex05_Othello
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Game game = new Game();
        }
    }
}
