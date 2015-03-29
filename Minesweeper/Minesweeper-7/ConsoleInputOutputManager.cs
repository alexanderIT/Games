namespace Minesweeper
{
    using System;
    using System.Linq;
    using Contracts;

    /// <summary>
    /// Manages the input and output operations of the game.
    /// </summary>
    public class ConsoleInputOutputManager : IInputOutputManager
    {
        public int Score { get; set; }

        public string PrintInitialMessage()
        {
            string welcomeMessage = "Welcome to the game “Minesweeper”. Try to reveal all cells without mines.";
            string commands = "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit  to quit  the game.'";
             

            Console.WriteLine(welcomeMessage + " " + commands);

            return welcomeMessage + " " + commands;

        }

        public void PrintGameField(IField gameField, bool hasBoomed)
        {
            if (hasBoomed)
            {
                PrintExplosionMessage(this.Score);

                return;
            }

            if (gameField == null)
            {
                throw new ArgumentNullException("Game field is null!");
            }

            Console.WriteLine();
            Console.WriteLine("     0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");

            for (int row = 0; row < gameField.Rows; row++)
            {
                Console.Write("{0} | ", row);
                for (int col = 0; col < gameField.Columns; col++)
                {
                    char currentSymbol = gameField[row, col];
                    PrintCurrentSymbol(currentSymbol, hasBoomed);
                }

                Console.WriteLine("|");
            }

            Console.WriteLine("   ---------------------");
        }

        public void PrintExplosionMessage(int score)
        {
            Console.WriteLine("\nBooom! You are killed by a mine!" +
                "You revealed {0} cells without mines.", score);
            Console.WriteLine();
        }

        public void PrintWinnerMessage()
        {
            Console.WriteLine("Congratulations! You are the WINNER!\n" +
                 "Please enter your name for the top scoreboard: ");
            Console.WriteLine();
        }

        public void PrintScoreBoard(IScoreBoard scoreBoard)
        {
            if (scoreBoard == null)
            {
                Console.WriteLine("Scoreboard is null!");
            }

            string generatedList = scoreBoard.Generate();
            Console.WriteLine(generatedList);
        }

        public void PrintInvalidCommandMessage()
        {
            Console.WriteLine("Invalid row/col entered! Try again!");

        }

        public void PrintQuitMessage()
        {
            Console.WriteLine("\nGood bye!\n");
        }

        public string GetUserInput()
        {
            Console.Write("Enter row and column: ");

            string input = Console.ReadLine();
            input = input.Trim();
            if (input.Length < 2)
            {
                Console.WriteLine("Enter two numbers - one for row, one for column");
            }
            else if (input.Length > 2)
            {
                Console.WriteLine("Your numbers should be two - one for row and one for column");
            }
            return input;
        }

        public string GetUserNickname()
        {
            Console.Write("Please enter your name: ");
            string playerNickname = Console.ReadLine();
            Console.WriteLine();

            return playerNickname;
        }

        private static void PrintCurrentSymbol(char currentSymbol, bool hasExploded)
        {
            if (!(hasExploded) && ((currentSymbol == char.MinValue) || (currentSymbol == '*')))
            {
                Console.Write(" ?");
            }
            else if (!(hasExploded) && (currentSymbol != char.MinValue) && (currentSymbol != '*'))
            {
                Console.Write(" {0}", currentSymbol);
            }
            else if ((hasExploded) && (currentSymbol == char.MinValue))
            {
                Console.Write(" -");
            }
            else if ((hasExploded) && (currentSymbol != char.MinValue))
            {
                Console.Write(" {0}", currentSymbol);
            }
        }

        public void Restart(IEngine engine)
        {
            Console.Clear();
            engine.Play();
        }
    }
}