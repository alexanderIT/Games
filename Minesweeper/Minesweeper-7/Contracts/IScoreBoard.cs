namespace Minesweeper.Contracts
{
    /// <summary>
    /// An interface for the score board of the game.
    /// </summary>
    public interface IScoreBoard
    {
        /// <summary>
        /// Adds a new player to the scoreboard.
        /// </summary>
        /// <param name="name">Player's nickname.</param>
        /// <param name="score">Player's score.</param>
        void AddPlayer(string name, int score);

        /// <summary>
        /// Generates the scoreboard with the specified number of top players and returns it as string.
        /// </summary>
        /// <returns>The generated scoreboard as string.</returns>
        string Generate();
    }
}
