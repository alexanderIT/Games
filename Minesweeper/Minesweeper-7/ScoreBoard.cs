namespace Minesweeper
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;

    using Wintellect.PowerCollections;

    /// <summary>
    /// Represents a scoreboard.
    /// </summary>
    public class ScoreBoard : IScoreBoard
    {
        private const int MaxShowedPlayersCount = 5;

        private readonly OrderedMultiDictionary<int, string> scoreBoard;

        /// <summary>
        /// Constructs an instance of the class.
        /// </summary>
        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        /// <summary>
        /// Adds a new player to the scoreboard.
        /// </summary>
        /// <param name="playerName">Player's nickname.</param>
        /// <param name="playerScore">Player's score.</param>
        public void AddPlayer(string playerName, int playerScore)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                throw new ArgumentNullException("Nickname cannot be null or empty!");
            }

            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new ArgumentException("Nickname cannot contain only white spaces!");
            }

            if (playerScore < 0)
            {
                throw new ArgumentOutOfRangeException("Score cannot be a negative value!");
            }

            if (!this.scoreBoard.ContainsKey(playerScore))
            {
                this.scoreBoard.Add(playerScore, playerName);
            }
            else
            {
                this.scoreBoard[playerScore].Add(playerName);
            }
        }

        /// <summary>
        /// Generates the scoreboard with the specified number of top players and returns it as string.
        /// </summary>
        /// <returns>The generated scoreboard as string.</returns>
        public string Generate()
        {
            if (this.scoreBoard.Values.Count == 0)
            {
                string emptyScoreBoard = "\n\rScoreboard empty!";

                return emptyScoreBoard;
            }
            else
            {
                StringBuilder scoreBoard = new StringBuilder();
                scoreBoard.Append("\n\rScoreboard:");

                int currentPlayer = 1;
                var orderedScoreDescending = this.scoreBoard.Keys.OrderByDescending(obj => obj.ToString());

                foreach (var key in orderedScoreDescending)
                {
                    foreach (var person in this.scoreBoard[key])
                    {
                        if (currentPlayer <= MaxShowedPlayersCount)
                        {
                            scoreBoard.Append(string.Format("\n\r{0}. {1} --> {2} cells", currentPlayer, person, key));
                            currentPlayer++;
                        }
                    }
                }

                scoreBoard.AppendLine();

                return scoreBoard.ToString();
            }
        }
    }
}
