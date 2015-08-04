using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace TheMillGame
{
    internal static class ScoreSystem
    {
        private static string scoreTablePath = @"..\..\ScoreTable.txt";
        private static List<string> scoreTable = new List<string>();

        /// Read the file where scores are stored. File is small enough to read it at once.
        public static void ReadWholeFile()
        {
            string line;

            if (!scoreTable.Any())
            {
                StreamReader reader = new StreamReader(scoreTablePath, Encoding.GetEncoding("windows-1251"));
                using (reader)
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Add each line in the List
                        scoreTable.Add(line);
                    }
                }
            }
        }

        // Print top 10
        public static void PrintTopScores()
        {
            Console.Clear();
            // Check if list is empty
            if (!scoreTable.Any())
            {
                ReadWholeFile();
            }

            for (int i = 0; i < scoreTable.Count; i++)
            {
                if (i % 2 == 0) { Console.ForegroundColor = ConsoleColor.Green; }
                else Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write(scoreTable[i].Substring(0, scoreTable[i].IndexOf(' ')).PadLeft(4));
                Console.Write(" - ");
                Console.WriteLine("{0}", scoreTable[i].Substring(scoreTable[i].IndexOf(' ') + 1).PadRight(15));
            }
        }

        // Write the score and player name into the file
        public static int Score(int gameWon)
        {
            int score;
            string playerName;
            score = 1;
            playerName = ReadPlayerName();
            // Check if list is empty
            if (!scoreTable.Any())
            {
                ReadWholeFile();
            }
            foreach (var item in scoreTable)
            {
                if (Regex.Match(item, @"\b" + playerName + @"\b").Success)
                {
                    string temp = Regex.Match(item, @"(\d) .*").Groups[1].ToString();
                    score += int.Parse(temp);
                }
            }
            SaveScoreToFile(score, playerName);

            return score;
        }

        // Reads player name, from the console
        static string ReadPlayerName()
        {
            Console.SetCursorPosition(45, 4);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("YOU'VE WON !");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(45, 5);
            Console.Write("Enter your name");
            Console.SetCursorPosition(45, 6);
            Console.Write(": ");
            string name = Console.ReadLine();

            return name;
        }

        //Get highest score
        public static int GetHighScore()
        {
            int score;
            string scoreText = string.Empty;

            // Check if list is empty
            if (!scoreTable.Any())
            {
                ReadWholeFile();
            }

            if (scoreTable.Any())
            {
                scoreText = scoreTable[0].Substring(0, scoreTable[0].IndexOf(' '));
                score = int.Parse(scoreText);
            }
            // Initially, when file is empty, 0 will be returned
            else
            {
                score = 0;
            }

            return score;
        }

        // Write List with scores and names to file
        public static void WriteToFile()
        {
            StreamWriter writer = new StreamWriter(scoreTablePath, false, Encoding.GetEncoding("windows-1251"));

            using (writer)
            {
                for (int i = 0; i < scoreTable.Count; i++)
                {
                    writer.WriteLine(scoreTable[i]);
                }
            }
        }

        // Insert score and name to the wright place in the table, and write it to the file
        public static void SaveScoreToFile(int currentScore, string playerName)
        {
            string scoreAndName = String.Concat(currentScore, ' ', playerName);
            string score;
            int scoreInTable;

            for (int i = 0; i < scoreTable.Count; i++)
            {
                if (i == 10)
                {
                    return;
                }

                score = scoreTable[i].Substring(0, scoreTable[i].IndexOf(' ')); //Throw an exception if space(' ') is at the start of the string
                scoreInTable = int.Parse(score);

                // Find the correct place of the current score
                if (currentScore > scoreInTable)
                {
                    if (scoreTable.Count == 10)
                    {
                        scoreTable.RemoveAt(9);
                    }

                    // Insert the current score at the correct place in the file
                    scoreTable.Insert(i, scoreAndName);
                    WriteToFile();
                    return;
                }
            }

            scoreTable.Add(scoreAndName);
            WriteToFile();
        }
    }
}
