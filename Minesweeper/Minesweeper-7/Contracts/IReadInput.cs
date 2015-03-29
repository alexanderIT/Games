namespace Minesweeper.Contracts
{
    using System;

    /// <summary>
    /// Manages the input operations of the game.
    /// </summary>
    public interface IReadInput
    {
        /// <summary>
        /// Gets the user nickname.
        /// </summary>
        /// <returns>Returns the user's nickname.</returns>
        string GetUserNickname();

        /// <summary>
        /// Gets the user input - command or coordinates (row and column).
        /// </summary>
        /// <returns>Returns the user input.</returns>
        string GetUserInput();
    }
}
