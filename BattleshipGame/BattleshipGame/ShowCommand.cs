namespace BattleshipGame
{
    using System;
    using BattleshipGame.Contracts;
    public class ShowCommand : ICommand
    {
        /// <summary>
        /// Receiver command
        /// </summary>
        protected IGameBoard ReceiveCommand;

        public char[,] GameBoardWhitShipsField { get; set; }

        /// <summary>
        /// Specific "show" command
        /// </summary>
        /// <param name="board">Receiver command.</param>
        public ShowCommand(IGameBoard board)
        {
            this.ReceiveCommand = board;
        }

        /// <summary>
        /// Execute the action of receiver.
        /// </summary>
        public void Execute()
        {
            this.ReceiveCommand.PrintBoardWhitShips(GameBoardWhitShipsField);
        }
    }
}
