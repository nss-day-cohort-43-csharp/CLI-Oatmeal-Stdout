using TabloidCLI.UserInterfaceManagers;
using System;

namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool testToken = true;

            while (testToken)
            {
                Console.WriteLine("Would you prefer dark mode or light mode?");
                Console.WriteLine("Enter for Dark Mode, 'L' for Light Mode: ");

                string ColorChoice = Console.ReadLine().ToLower();

                if (ColorChoice == "l")
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    testToken = false;
                }
                else if (string.IsNullOrEmpty(ColorChoice))
                {
                    testToken = false;
                }
                else
                {
                    Console.WriteLine("That is an invalid selection, please try again");
                }
            }
            



            Console.WriteLine("Welcome to Tabloid CLI");
            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
    }
}
