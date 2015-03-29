namespace BattleshipGame.Contracts
{
    using System;
    /// <summary>
    /// Command interface
    /// </summary>
    public interface ICommand
    {
        char[,] GameBoardWhitShipsField { get; set; }
        void Execute();
    }
}
