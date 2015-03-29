namespace BattleshipGame.Contracts
{
    using System;
    /// <summary>
    /// IReadInput interface
    /// </summary>
    public interface IReadInput
    {
        string playerInput { get; set; }
        int xCoord { get; set; }
        int yCoord { get; set; }
        bool validInput { get; set; }
        bool IsCommand { get; set; }
        ICommand Command { get; set; }
        void ExecuteShowCommand();
        void PrintError(string error);
        void ReadAndValidateInput();
    }
}
