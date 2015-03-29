namespace BattleshipGame.Contracts
{
    using System;
    /// <summary>
    /// IGameBoard interface
    /// </summary>
    public interface IGameBoard
    {
        char[,] GameBoardCPUField { get; set; }
        char[,] GameBoardPlayerField { get; set; }
        bool GameComplete { get; set; }
        int Shots { get; set; }
        void EmptyGameBoardField();
        void PrintBoard(char[,] GameBoardCPUField, char[,] GameBoardPlayerField, int x, int y);
        void PrintBoardWhitShips(char[,] GameBoardCPUField);
    }
}
