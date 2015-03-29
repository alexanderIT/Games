namespace BattleshipGame
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// IGenarateRandomBattleShip interface
    /// </summary>
    public interface IGenarateRandomBattleShip
    {
        void ChangeDestroyedPossitionAndTheNeighborhood(string coordinate);
        char[,] FillShipsInBoard(List<List<string>> coordinates, char[,] gameBoard);
        System.Collections.Generic.List<string> GenerateFiveSquareShip();
        System.Collections.Generic.List<string> GenerateFourSquareDestroyer();
        System.Collections.Generic.List<List<string>> GenerateShipsForMatrix();
        bool IsPositionInTheMatrix(int xCoordinate, int yCoordinate);
        void RemoveUsedElementsFromList(int xCoordinate, int yCoordinate);
    }
}
