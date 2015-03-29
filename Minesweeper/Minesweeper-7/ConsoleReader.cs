namespace Minesweeper
{
    using System;
    using Contracts;

    public class ConsoleReader : IReadInput
    {
        /// <summary>
        /// Reads the user's nickname from the console.
        /// </summary>
        /// <returns>Returns the user's nickname.</returns>
        public string GetUserNickname()
        {
            Console.Write("Please enter your name: ");
            string playerNickname = Console.ReadLine();
            Console.WriteLine();

            return playerNickname;
        }

        /// <summary>
        /// Reads the user input from the console - command or coordinates (row and column).
        /// </summary>
        /// <returns>Returns the user input.</returns>
        public string GetUserInput()
        {
            Console.Write("Enter row and column: ");

            string input = Console.ReadLine();
            input = input.Trim();
            if (input.Length < 2)
            {
                Console.WriteLine("Enter two numbers - one for row, one for column !");
            }
            else if (input.Length > 2)
            {
                Console.WriteLine("Numbers should be two - one for row and one for column !");
            }
            return input;
        }
    }
}
