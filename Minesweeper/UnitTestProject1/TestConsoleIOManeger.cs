namespace MinesweeperTests
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    using Minesweeper;
    using Minesweeper.Contracts;

    using TestMinesweeper;

    [TestClass]
    public class TestIOManager
    {
        private readonly IInputOutputManager inputOutputManager = new ConsoleInputOutputManager();

        [TestMethod]
        public void TestInitialMessage()
        {
            string welcomeMessage = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines.";
            string commands = "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            string outputMessage = inputOutputManager.PrintInitialMessage();
            Assert.AreEqual(outputMessage, (welcomeMessage + " " + commands));
        }

        [TestMethod]
        public void TestPrintExplosionMessage()
        {
            using (ConsoleRedirector consoleRedirector = new ConsoleRedirector())
            {
                int score = 5;
                Assert.IsFalse(consoleRedirector.ToString().Contains("\nBooom! You are killed by a mine!You revealed " +
                    score + " cells without mines."));
                inputOutputManager.PrintExplosionMessage(score);
                Assert.IsTrue(consoleRedirector.ToString().Contains("\nBooom! You are killed by a mine!You revealed " +
                    score + " cells without mines."));
            }
        }

        [TestMethod]
        public void TestPrintWinnerMessage()
        {
            using (ConsoleRedirector cr = new ConsoleRedirector())
            {
                Assert.IsFalse(cr.ToString().Contains("Congratulations! You are the WINNER!\nPlease enter your name for the top scoreboard: "));
                inputOutputManager.PrintWinnerMessage();
                Assert.IsTrue(cr.ToString().Contains("Congratulations! You are the WINNER!\nPlease enter your name for the top scoreboard: "));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPrintScoreBoardOnNull()
        {
            inputOutputManager.PrintScoreBoard(null);
        }

        [TestMethod]
        public void TestPrintInvalidCommandMessage()
        {
            using (ConsoleRedirector cr = new ConsoleRedirector())
            {
                Assert.IsFalse(cr.ToString().Contains("Invalid row/col entered! Try again!"));
                inputOutputManager.PrintInvalidCommandMessage();
                Assert.IsTrue(cr.ToString().Contains("Invalid row/col entered! Try again!"));
            }
        }

        [TestMethod]
        public void TestPrintQuitMessage()
        {
            using (ConsoleRedirector cr = new ConsoleRedirector())
            {
                Assert.IsFalse(cr.ToString().Contains("\nGood bye!\n"));
                inputOutputManager.PrintQuitMessage();
                Assert.IsTrue(cr.ToString().Contains("\nGood bye!\n"));
            }
        }

        [TestMethod]
        public void TestGetUserInput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("12",
                    Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    inputOutputManager.GetUserInput();

                    string expected = string.Format(
                        "Enter row and column: ", Environment.NewLine);
                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetUserInputNotValidInputWithOnlyOneNumber()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("1",
                    Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    inputOutputManager.GetUserInput();

                    string expected = string.Format(
                        "Enter row and column: ", Environment.NewLine);
                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }

        [TestMethod]
        public void TestGetUserNickname()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("Test Console.ReadLine()",
                    Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    inputOutputManager.GetUserNickname();

                    string expected = string.Format("Please enter your name: ");
                    string expected2 = string.Format(Environment.NewLine);
                    Assert.AreEqual<string>(expected + expected2, sw.ToString());
                }
            }
        }
    }
}

