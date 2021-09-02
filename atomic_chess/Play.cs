using System;
using System.Windows.Forms;

namespace atomic_chess
{
    static class Play
    {
        //    public static Piece.Color turn { set; get; }

        //    public static bool gameOver = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new atomicChess());

            //while (!gameOver)
            //{
            //    if (turn == Piece.Color.black)
            //    {
            //        //fa mutarea pt negru
            //        turn = Piece.Color.white;
            //    }

            //    if (turn == Piece.Color.white)
            //    {
            //        //fa mutarea pt alb
            //        turn = Piece.Color.black;
            //    }

        }
    }
}
