namespace Minesweeper
{
    using System;
    using System.Linq;
    using Contracts;

    /// <summary>
    /// Represents the main application from which will start the game.
    /// </summary>
    public class Minesweeper
    {
        internal static void Main()
        {
            FieldFactory minesweeperFactory = new MinesweeperField();
            IField minesweeperField = minesweeperFactory.CreateField();

            FieldFactory playingFieldFactory = new PlayingField();
            IField playingField = playingFieldFactory.CreateField();

            IRenderer consoleRenderer = new ConsoleRenderer();
            IReadInput inputReader = new ConsoleReader();

            //Console.WriteLine(minesweeperField.ToString());

            IEngine engine = new Engine(minesweeperField, playingField, consoleRenderer, inputReader);
            engine.Play();
        }
    }
}
