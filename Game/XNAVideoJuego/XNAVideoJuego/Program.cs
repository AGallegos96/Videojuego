using System;

namespace XNAVideoJuego
{
#if WINDOWS
    static class Program
    {
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Window.Title = "Abandagi - The Game";
                game.Run();
            }
        }
    }
#endif
}

