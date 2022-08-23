using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CheckersGameUI
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameManager checkersGame = new GameManager();
            checkersGame.Run();
        }
    }
}