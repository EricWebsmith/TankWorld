using System;
using TankWorld.UI.Amination;

namespace TankWorld.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TankWorld.GameManager.PlayerFactory.Initialise();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new BatchMatchForm());

            //MyGame myGame = new MyGame(1200, 650);
            //myGame.Run();
        }
    }
}
