using System;

namespace ImpossibleGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ImpossibleGame game = new ImpossibleGame())
            {
                game.Run();
            }
        }
    }
#endif
}

