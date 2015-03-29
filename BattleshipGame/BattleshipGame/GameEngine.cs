namespace BattleshipGame
{
    using System;
    using System.Threading;
    using BattleshipGame.Contracts;
    public class GameEngine
    {
        private IGameBoard Board { get; set; }
        private GenarateRandomBattleShip ShipsGenerator { get; set; }
        private IReadInput ReadInput { get; set; }
        private ICommand Command { get; set; }
        private char[,] BoardWithShips { get; set; }

        public GameEngine()
        {
            this.Board = new GameBoard(10, 10);
            this.ShipsGenerator = new GenarateRandomBattleShip();
            this.ReadInput = new ReadInput();
            this.BoardWithShips = TakeShips();
        }

        public GameEngine(IGameBoard board, GenarateRandomBattleShip ships, IReadInput readInput)
        {
            this.Board = board;
            this.ShipsGenerator = ships;
            this.ReadInput = readInput;
            this.BoardWithShips = TakeShips();
        }

        /// <summary>
        /// Return char array with random placed ships.
        /// </summary>
        /// <returns></returns>
        private char[,] TakeShips()
        {
            this.Board.EmptyGameBoardField();
            this.Board.PrintBoard(Board.GameBoardCPUField, Board.GameBoardPlayerField, 0, 0);
            this.ShipsGenerator = new GenarateRandomBattleShip();

            var listOfShips = ShipsGenerator.GenerateShipsForMatrix();
            var boardWithShips = ShipsGenerator.FillShipsInBoard(listOfShips, Board.GameBoardCPUField);

            return boardWithShips;
        }

        /// <summary>
        /// Specific instance of ICommand interface.
        /// </summary>
        public void ShowCommand()
        {
            this.Command = new ShowCommand(Board);
            Command.GameBoardWhitShipsField = BoardWithShips;

            this.ReadInput.Command = Command;
            this.ReadInput.ExecuteShowCommand();
        }

        /// <summary>
        /// Read input from console, check if all ships is hitted and print i console the current state of game field.
        /// </summary>
        public void RunGame()
        {
            while (true)
            {
                if (Board.GameComplete)
                {
                    Console.WriteLine("Well done! You completed the game in {0} shots", Board.Shots);
                    break;
                }

                this.ReadInput.ReadAndValidateInput();

                if (ReadInput.IsCommand)
                {
                    this.ShowCommand();
                    Console.ReadLine();
                    continue;
                }

                this.Board.PrintBoard(BoardWithShips, Board.GameBoardPlayerField, ReadInput.xCoord, ReadInput.yCoord);
            }
        }
    }
}
