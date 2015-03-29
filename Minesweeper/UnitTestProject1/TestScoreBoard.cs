namespace MinesweeperTests
{
    using System;
    using Minesweeper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestScoreBoard
    {
        [TestMethod]
        public void TestAddPlayerInScoreBoard_ValidInput()
        {
            string playerName = "Pesho";
            int playerScore = 7;
            int currentPlayer = 1;
            string output = "\n\rScoreboard:\n\r" + currentPlayer + ". " + playerName + " --> " + playerScore + " cells" + Environment.NewLine;

            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.AddPlayer(playerName, playerScore);
            Assert.AreEqual(output, scoreBoard.Generate());

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddPlayerInScoreBoard_NameIsNull()
        {
            int score = 7;

            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.AddPlayer(null, score);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddPlayerInScoreBoard_NameIsEmpty()
        {
            int score = 7;
            string name = string.Empty;

            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.AddPlayer(name, score);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddPlayerInScoreBoard_NameIsOnlyWhiteSpaces()
        {
            int score = 7;
            string name = "  ";

            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.AddPlayer(name, score);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPlayerInScoreBoard_ScoreIsNegative()
        {
            int score = -2;
            string name = "Sasho";

            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.AddPlayer(name, score);
        }

        [TestMethod]
        public void TestGenerateScoreBoard()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            int currentPlayer = 1;
            string playerName = "Sasho";
            int playerScore = 10;

            string output = "\n\rScoreboard:\n\r" + currentPlayer + ". Sasho --> " + playerScore + " cells" + Environment.NewLine;

            scoreBoard.AddPlayer(playerName, playerScore);
            Assert.AreEqual(output, scoreBoard.Generate());
        }
    }
}
