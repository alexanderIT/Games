using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMillGame
{
    class OptionMenu
    {
        private static void DrawLine()
        {
            Console.WriteLine(new string('*', 20));
        }

        private static void DrawMenuList()
        {
            DrawLine();
            Console.WriteLine("1. RESUME");
            Console.WriteLine("2. SAVE");
            Console.WriteLine("3. SAVE AND EXIT");
            Console.WriteLine("4. SAUND ON/OFF");
            Console.WriteLine("0. EXIT");
            DrawLine();
            Console.Write("Select an option: ");
        }
        private static void ErrorMasage()
        {
            Console.WriteLine("Invalid choise! Press any key: ");
        }

        static void Menu()
        {
            ConsoleKeyInfo escButton = Console.ReadKey();
            if (escButton.Key == ConsoleKey.Escape)
            {
                byte choice = 0;
                DrawMenuList();
                while (!byte.TryParse(Console.ReadLine(), out choice) || (choice > 4))
                {
                    ErrorMasage();
                    Console.ReadKey();
                    Console.Clear();
                    DrawMenuList();
                }
                if (choice == 0)
                {
                    Console.WriteLine("//Logic for exit the game!");
                }
                else if (choice == 1)
                {
                    Console.WriteLine("//Logic for returning to the game!");
                }
                else if (choice == 2)
                {
                    Console.WriteLine("//Logic for saving the game!");
                }
                else if (choice == 3)
                {
                    Console.WriteLine("//Logic for save and exit the game the game!");
                }
                else if (choice == 4)
                {
                    Console.WriteLine("//Logic for sound ON/OFF.");
                }
            }
        }
    }
}
