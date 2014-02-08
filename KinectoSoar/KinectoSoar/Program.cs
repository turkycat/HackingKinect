using System;

namespace KinectoSoar
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Soar game = new Soar())
            {
                game.Run();
            }
        }
    }
#endif
}

