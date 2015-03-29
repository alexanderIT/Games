namespace Minesweeper
{
    using System;
    using Contracts;

    /// <summary>
    /// Renders the UI to the console
    /// </summary>
    public class ConsoleRenderer : IRenderer
    {
        /// <summary>
        /// Renders the initial message to the console - welcome message and commands list.
        /// </summary>
        public void RenderInitialMessage()
        {
            string welcomeMessage = @"   Welcome to the game “Minesweeper”. Try to reveal all cells without mines.";
            string commands = @"   Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit'      to quit  the game.";
            Console.WriteLine();
            
            Console.WriteLine(welcomeMessage + Environment.NewLine + commands);
        }

        /// <summary>
        /// Renders the game field to the console.
        /// </summary>
        /// <param name="gameField">The field to be rendered.</param>
        /// <param name="hasBoomed">Flag if player has exploded.</param>
        public void RenderGameField(IField gameField, bool hasBoomed)
        {
            if (gameField == null)
            {
                throw new ArgumentNullException("Game field is null!");
            }

            Console.WriteLine(gameField.ToString());
        }

        /// <summary>
        /// Renders an explosion message to the console when the user steps on a mine.
        /// </summary>
        public void RenderExplosionMessage(int score)
        {
            Console.WriteLine(Environment.NewLine + "Booom! You are killed by a mine!" + 
                Environment.NewLine + "You revealed {0} cells without mines.", score);
        }

        /// <summary>
        /// Renders a message to the console when the user completes the whole game.
        /// </summary>
        public void RenderWinnerMessage()
        {
            Console.WriteLine(Environment.NewLine + "Congratulations! You are the WINNER!" +
                 Environment.NewLine + "Please enter your name for the top scoreboard: ");
        }

        /// <summary>
        /// Renders the scoreboard to the console.
        /// </summary>
        /// <param name="scoreBoard">Scoreboard instance for the particular game.</param>
        public void RenderScoreBoard(IScoreBoard scoreBoard)
        {
            if (scoreBoard == null)
            {
                throw new ArgumentNullException("Scoreboard is null!");
            }

            string generatedList = scoreBoard.Generate();

            Console.WriteLine(generatedList);
        }

        /// <summary>
        /// Renders an informational message to the console on invalid user input.
        /// </summary>
        public void RenderInvalidCommandMessage()
        {
            Console.WriteLine("Invalid row/col entered! Try again!");
        }

        /// <summary>
        /// Renders the end message to the console when the user want to quits the game.
        /// </summary>
        public void RenderQuitMessage()
        {
            Console.WriteLine(Environment.NewLine + "Good bye!" + Environment.NewLine);
        }

        public void RenderCommandsMessage()
        {
            string commands = "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game. ";
            Console.WriteLine(commands);
             

        }
    }
}
