namespace TheMillGame
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Media;
    using System.Text;
    using System.Threading;

    public class TheMillGame
    {
        private const int WindowWidth = 90;
        private const int WindowHeight = 40;
        private const int MaxNameLenght = 10;

        #region Sound Paths
        private const string WinSound = @"../../Sounds/win.wav";
        private const string KoramSound = @"../../Sounds/koram.wav";
        private const string MenuNavSound = @"../../Sounds/menu_nav.wav";
        private const string MoveSound = @"../../Sounds/move.wav";
        private const string TakeOffSound = @"../../Sounds/take_off_pul.wav";
        private const string WaitingSound = @"../../Sounds/waiting.wav";
        private const string WrongInputSound = @"../../Sounds/wrong_input.wav";
        private const string StartMenuSound = @"../../Sounds/start_menu.wav";
        #endregion

        #region Text Messages Paths (Wallpaper)
        private const string SoundMsg = "../../TextMessages/sound.txt";
        private const string GoodByeMsg = "../../TextMessages/goodbye.txt";
        private const string WinMsg = "../../TextMessages/win.txt";
        private const string LogoMsg = "../../TextMessages/GameLogo.txt";
        #endregion

        // The menu
        #region Menu Constants
        private static readonly char StartMenuCursor = '\u25BA';
        private static int defaultCursorCol = (WindowWidth / 2) - (WindowWidth / 10);
        private static int defaultCursorStartRow = WindowHeight / 2;
        private static int defaultCurrsorEndRow = defaultCursorStartRow + 7;
        private static int cursorCurrentRow = defaultCursorStartRow;
        private static int option = 1;
        #endregion

        private static bool soundON = true;
        private static SoundPlayer soundPlayer = new SoundPlayer();
        private static RealPlayer firstPlayer;
        private static Player secondPlayer;
        private static Board gameBoard;

        private static byte[, ,] AllPossibleMills = {
             { { 0, 0 }, { 0, 1 }, { 0, 2 } },
             { { 1, 0 }, { 1, 1 }, { 1, 2 } }, 
             { { 2, 0 }, { 2, 1 }, { 2, 2 } }, 
             { { 0, 5 }, { 0, 6 }, { 0, 7 } }, 
             { { 1, 5 }, { 1, 6 }, { 1, 7 } }, 
             { { 2, 5 }, { 2, 6 }, { 2, 7 } }, 
             { { 0, 0 }, { 0, 3 }, { 0, 5 } }, 
             { { 1, 0 }, { 1, 3 }, { 1, 5 } }, 
             { { 2, 0 }, { 2, 3 }, { 2, 5 } }, 
             { { 0, 2 }, { 0, 4 }, { 0, 7 } }, 
             { { 1, 2 }, { 1, 4 }, { 1, 7 } }, 
             { { 2, 2 }, { 2, 4 }, { 2, 7 } }, 
             { { 0, 1 }, { 1, 1 }, { 2, 1 } }, 
             { { 0, 4 }, { 1, 4 }, { 2, 4 } }, 
             { { 0, 3 }, { 1, 3 }, { 2, 3 } }, 
             { { 0, 6 }, { 1, 6 }, { 2, 6 } }
                                                   };

        public static void Main()
        {
            ResetConsoleProperties();
            StartMenu();

            Console.SetCursorPosition(0, WindowHeight - 1);
        }

        public static void ResetConsoleProperties()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Nine Men\'s Morris";

            WindowSize(WindowWidth, WindowHeight);

            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        // Sets the width and the height of the Console
        public static void WindowSize(int width, int height)
        {
            Console.BufferWidth = Console.WindowWidth = width;
            Console.BufferHeight = Console.WindowHeight = height;
        }

        private static void StartMenu()
        {
            PrintLogo(LogoMsg);
            PrintStartMenuOptions();

            Console.SetCursorPosition(defaultCursorCol, defaultCursorStartRow);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(StartMenuCursor);

            while (true)
            {
                GetCurrsorDirections();
            }
        }

        private static void PrintLogo(string file)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            StreamReader logoText = new StreamReader(file);
            using (logoText)
            {
                string line = logoText.ReadLine();
                int logoRow = 0;

                while (line != null)
                {
                    Console.SetCursorPosition((WindowWidth / 2) - (line.Length / 2),
                        (WindowHeight / 5) + logoRow - 5);
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '=')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write('\u2588');
                        }
                        else if (line[i] == '*')
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write('\u2588');
                        }
                        else if (line[i] == '!')
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("│");
                        }
                        else if (line[i] == '.')
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write('\u2500');
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(line[i]);
                        }
                    }

                    line = logoText.ReadLine();
                    logoRow++;
                }
            }
        }

        private static void PrintStartMenuOptions()
        {
            string[] menuItems = { "START GAME", "LOAD GAME", "HIGH SCORES", "SOUND", "EXIT" };

            Console.ForegroundColor = ConsoleColor.Green;
            for (int item = 0, row = 0; item < menuItems.Length; item++, row += 2)
            {
                Console.SetCursorPosition((WindowWidth / 2) - (menuItems[0].Length / 2),
                    WindowHeight / 2 + row);
                Console.Write(menuItems[item]);
            }
        }

        private static void GetCurrsorDirections()
        {
            while (Console.KeyAvailable)
            {
                Console.SetCursorPosition(0, 0);
                ConsoleKeyInfo key = Console.ReadKey();

                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                PlaySound(MenuNavSound, false, soundON);

                if (key.Key == ConsoleKey.DownArrow && cursorCurrentRow < defaultCurrsorEndRow)
                {
                    Console.SetCursorPosition(defaultCursorCol, cursorCurrentRow);
                    Console.Write(' ');
                    cursorCurrentRow += 2;
                    Console.SetCursorPosition(defaultCursorCol, cursorCurrentRow);
                    Console.Write(StartMenuCursor);
                    option++;
                    Console.SetCursorPosition(0, 0);
                }
                else if (key.Key == ConsoleKey.UpArrow && cursorCurrentRow > defaultCursorStartRow)
                {
                    Console.SetCursorPosition(defaultCursorCol, cursorCurrentRow);
                    Console.Write(' ');
                    cursorCurrentRow -= 2;
                    Console.SetCursorPosition(defaultCursorCol, cursorCurrentRow);
                    Console.Write(StartMenuCursor);
                    option--;
                    Console.SetCursorPosition(0, 0);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    PlaySound(MoveSound, false, soundON, 1000);

                    switch (option)
                    {
                        case 1:
                            {
                                Console.Clear();
                                PlayGame();
                                break;
                            }

                        case 2:
                            {
                                Console.Clear();
                                Console.WriteLine("Loading game!");

                                // TODO: Implement method for loading a game   
                                break;
                            }

                        case 3:
                            {
                                Console.Clear();
                                Console.WriteLine("HighScores");
                                // PrintHighScores();
                                ScoreSystem.PrintTopScores();

                                Console.ReadLine();
                                Console.Clear();
                                StartMenu();
                                break;
                            }

                        case 4:
                            {
                                byte chSound;
                                do
                                {
                                    Console.Clear();
                                    PrintTextFileToConsole(SoundMsg);
                                    Console.SetCursorPosition(0, WindowHeight - 1);
                                    Console.Write("Press 1 for ON or 2 for OFF: ");
                                }
                                while (!byte.TryParse(Console.ReadLine(), out chSound) &&
                                    chSound == 0 || (chSound != 1 && chSound != 2));
                                soundON = chSound == 1 ? true : false;
                                Console.Clear();
                                cursorCurrentRow = defaultCursorStartRow;
                                option = 1;

                                StartMenu();
                                break;
                            }

                        case 5:
                            {
                                Console.Clear();
                                ExitGame(GoodByeMsg);
                                break;
                            }
                    }
                }
            }
        }

        private static void PrintTextFileToConsole(string file)
        {
            Console.Write(File.ReadAllText(file));
        }

        private static void PlayGame()
        {
            byte gameMode;
            do
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.Write("Press 1 for singleplayer or 2 for multiplayer: ");
            }
            while (!byte.TryParse(Console.ReadLine(), out gameMode) || (gameMode != 1 && gameMode != 2));

            List<string> playersName = (InputPlayersNames(gameMode));

            // SinglePlayer is started when the name of the 2nd player is 'PC'
            if (gameMode == 1)
            {
                playersName.Add("PC");
                secondPlayer = new Robot(new Piece(), playersName[1]);
            }
            else
            {
                secondPlayer = new RealPlayer(new Piece(), playersName[1], 0);
            }

            gameBoard = new Board();
            gameBoard.DrawBoard();

            firstPlayer = new RealPlayer(new Piece(), playersName[0], 0);

            firstPlayer.piece.LoadPiecesPositionsFromBoard(gameBoard);
            secondPlayer.piece.LoadPiecesPositionsFromBoard(gameBoard);

            firstPlayer.Display = new Position(35, 11);
            secondPlayer.Display = new Position(47, 17);

            secondPlayer.piece.PiecesColor = ConsoleColor.Red;

            PiecePlacing();
            PieceMoving();
        }

        private static void ExitGame(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintTextFileToConsole(msg);
            Console.SetCursorPosition(0, WindowHeight - 1);
            Environment.Exit(0);
        }

        private static void PlaySound(string soundPath, bool repeat, bool onOff, int time = 0)
        {
            if (onOff)
            {
                soundPlayer.SoundLocation = soundPath;

                if (!repeat)
                {
                    soundPlayer.Play();
                    Thread.Sleep(time);
                }
                else
                {
                    soundPlayer.PlayLooping();
                }
            }
        }

        // Place the pieces on the board at begining
        private static void PiecePlacing()
        {
            int count = 0;

            gameBoard.DrawPlayerUnplacedPieces(firstPlayer);
            gameBoard.DrawPlayerName(firstPlayer, WindowWidth, MaxNameLenght);
            gameBoard.DrawPlayerUnplacedPieces(secondPlayer);
            gameBoard.DrawPlayerName(secondPlayer, WindowWidth, MaxNameLenght);

            while (count < 18)
            {
                try
                {
                    Console.SetCursorPosition(WindowWidth / 2 - 5, WindowHeight - 2);  // check also "finally" block below
                    if (count % 2 == 0)
                    {
                        Check(firstPlayer, secondPlayer);
                    }
                    else
                    {
                        Check(secondPlayer, firstPlayer);
                    }

                    count++;
                }
                catch (Exception)
                {
                    /* Съобщението да се изпрати на друга функция, която да го визуализира по подходящ начин
                    Console.Write(e.Message);*/
                    PlaySound(WrongInputSound, false, soundON, 800);
                }
                finally
                {
                    // Clear the previous text input
                    Console.SetCursorPosition(WindowWidth / 2 - 5, WindowHeight - 2);
                    Console.Write(new string(' ', 15));
                }
            }
        }

        // Move the pieces after they are placed on the board
        private static void PieceMoving()
        {
            long count = 0;

            while (true)
            {
                try
                {
                    string input = string.Empty;
                    PlaySound(WaitingSound, true, soundON);

                    // брой оставащи пулове**************************************************
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition((WindowWidth / 2) - 5, (WindowHeight / 2) - 5);
                    Console.Write(firstPlayer.piece.Count);
                    Console.SetCursorPosition((WindowWidth / 2) + 5, (WindowHeight / 2) + 1);
                    Console.Write(secondPlayer.piece.Count);

                    Console.SetCursorPosition(WindowWidth / 2 - 5, WindowHeight - 2);  // check also "finally" block below

                    if (count % 2 == 0)
                    {
                        Console.ForegroundColor = firstPlayer.piece.PiecesColor;
                        Console.Write("{0}: ", firstPlayer.Name);
                        input = ReadString(4);
                        bool[,] playerPositionTemp = (bool[,])firstPlayer.piece.positionsOnBoard.Clone();
                        firstPlayer.piece.Move(input, secondPlayer.piece, gameBoard);

                        if (CheckForMills(firstPlayer.piece, playerPositionTemp))
                        {
                            PlaySound(KoramSound, false, soundON, 3100);
                            while (true)
                            {
                                try
                                {
                                    Console.SetCursorPosition(WindowWidth / 2 - 5, WindowHeight - 2);
                                    Console.Write(new string(' ', 15));

                                    Console.SetCursorPosition(WindowWidth / 2 - 5, WindowHeight - 2);
                                    Console.ForegroundColor = firstPlayer.piece.PiecesColor;
                                    Console.Write("{0}: ", firstPlayer.Name);

                                    string remove = ReadString(2);
                                    secondPlayer.piece.RemovePiece(secondPlayer.piece.GetPositionFromInput(remove), firstPlayer.piece);
                                    PlaySound(TakeOffSound, false, soundON, 1350);
                                    break;
                                }
                                catch (Exception)
                                {
                                    PlaySound(WrongInputSound, false, soundON, 800);
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            PlaySound(MoveSound, false, soundON, 1350);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = secondPlayer.piece.PiecesColor;
                        Console.Write("{0}: ", secondPlayer.Name);

                        if (secondPlayer is Robot)
                        {
                            input = (secondPlayer as Robot).GenerateInputForMoving(firstPlayer.piece);
                        }
                        else
                        {
                            input = ReadString(4);
                        }

                        bool[,] playerPositionTemp = (bool[,])secondPlayer.piece.positionsOnBoard.Clone();
                        secondPlayer.piece.Move(input, firstPlayer.piece, gameBoard);

                        if (CheckForMills(secondPlayer.piece, playerPositionTemp))
                        {
                            PlaySound(KoramSound, false, soundON, 3100);
                            while (true)
                            {
                                try
                                {
                                    Console.SetCursorPosition(WindowWidth / 2 - 5, WindowHeight - 2);
                                    Console.Write(new string(' ', 15));

                                    Console.SetCursorPosition(WindowWidth / 2 - 5, WindowHeight - 2);
                                    Console.ForegroundColor = secondPlayer.piece.PiecesColor;
                                    Console.Write("{0}: ", secondPlayer.Name);

                                    string remove = String.Empty;
                                    if (secondPlayer is Robot)
                                    {
                                        remove = (secondPlayer as Robot).GenerateInputForRemoving(AllPossibleMills, firstPlayer.piece);
                                    }
                                    else
                                    {
                                        remove = ReadString(2);
                                    }

                                    firstPlayer.piece.RemovePiece(firstPlayer.piece.GetPositionFromInput(remove), secondPlayer.piece);
                                    PlaySound(TakeOffSound, false, soundON, 1350);
                                    break;
                                }
                                catch (Exception)
                                {
                                    if (!(secondPlayer is Robot))
                                    {
                                        PlaySound(WrongInputSound, false, soundON, 800);
                                    }
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            PlaySound(MoveSound, false, soundON, 1350);
                        }

                    }

                    GameOver(firstPlayer, secondPlayer);

                    count++;
                }
                catch (Exception)
                {
                    /* Съобщението да се изпрати на друга функция, която да го визуализира по подходящ начин
                    Console.Write(e.Message);*/
                    if (!(secondPlayer is Robot))
                    {
                        PlaySound(WrongInputSound, false, soundON, 800);
                    }
                }
                finally
                {
                    // Clear the previous text input
                    Console.SetCursorPosition(WindowWidth / 2 - 5, WindowHeight - 2);
                    Console.Write(new string(' ', 15));
                }
            }
        }

        private static string Check(Player p1, Player p2)
        {
            Console.ForegroundColor = p1.piece.PiecesColor;
            Console.Write("{0}: ", p1.Name);
            PlaySound(WaitingSound, true, soundON);

            string input = String.Empty;
            if (p1 is Robot)
            {
                input = (p1 as Robot).GenerateInputForPlacing(AllPossibleMills, p2.piece);
            }
            else
            {
                input = ReadString(2);
            }

            // positions temp for this player
            bool[,] playerPositionTemp = (bool[,])p1.piece.positionsOnBoard.Clone();

            p1.piece.curPos = p1.piece.GetPositionFromInput(input);
            p1.piece.DrawPieceAt(p1.piece.GetPositionFromInput(input), p1.piece.PiecesColor, p2.piece);
            p1.piece.positionsOnBoard[p1.piece.curPos.x, p1.piece.curPos.y] = true;

            gameBoard.DrawPlayerUnplacedPieces(p1);
            gameBoard.DrawPlayerUnplacedPieces(p2);

            // Check for "mills"
            if (CheckForMills(p1.piece, playerPositionTemp))
            {
                PlaySound(KoramSound, false, soundON, 3100);
                while (true)
                {
                    try
                    {
                        Console.SetCursorPosition(WindowWidth / 2 - 5, WindowHeight - 2);
                        Console.Write(new string(' ', 15));

                        Console.SetCursorPosition(WindowWidth / 2 - 5, WindowHeight - 2);
                        Console.ForegroundColor = p1.piece.PiecesColor;
                        Console.Write("{0}: ", p1.Name);

                        string remove = String.Empty;
                        if (p1 is Robot)
                        {
                            remove = (p1 as Robot).GenerateInputForRemoving(AllPossibleMills, p2.piece);
                        }
                        else
                        {
                            remove = ReadString(2);
                        }

                        p2.piece.RemovePiece(p2.piece.GetPositionFromInput(remove), p1.piece);
                        PlaySound(TakeOffSound, false, soundON, 1350);
                        break;
                    }
                    catch (Exception)
                    {
                        PlaySound(WrongInputSound, false, soundON, 800);
                        continue;
                    }
                }
            }
            else
            {
                PlaySound(MoveSound, false, soundON, 1350);
            }

            GameOver(p1, p2);

            return input;
        }

        private static void GameOver(Player p1, Player p2)
        {
            /*********************************************************/
            bool winP1 = p2.Loose(p1.piece.positionsOnBoard);
            bool winP2 = p1.Loose(p2.piece.positionsOnBoard);
            if (winP1 || winP2)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;
                PrintTextFileToConsole(WinMsg);
                if (winP1)
                {
                    Console.ForegroundColor = p1.piece.PiecesColor;
                    Console.SetCursorPosition((WindowWidth - p1.Name.Length) / 2, 0);
                    Console.Write("{0} wins!", p1.Name);
                }

                if (winP2)
                {
                    Console.ForegroundColor = p2.piece.PiecesColor;
                    Console.SetCursorPosition((WindowWidth - p2.Name.Length) / 2, 0);
                    Console.Write("{0} wins!", p2.Name);
                }

                PlaySound(WinSound, false, soundON, 1350);
                Console.ReadLine();
                Console.Clear();
                cursorCurrentRow = defaultCursorStartRow;
                option = 1;
                StartMenu();
                // да се измисли нещо за изход
            }
            /*********************************************************/
        }

        // Return true if mills are happen
        private static bool CheckForMills(Piece player, bool[,] playerPositionTemp)
        {
            for (int n = 0; n < AllPossibleMills.GetLength(0); n++)
            {
                if ((player.positionsOnBoard[AllPossibleMills[n, 0, 0], AllPossibleMills[n, 0, 1]] &&
                    player.positionsOnBoard[AllPossibleMills[n, 1, 0], AllPossibleMills[n, 1, 1]] &&
                    player.positionsOnBoard[AllPossibleMills[n, 2, 0], AllPossibleMills[n, 2, 1]]) &&
                    !(playerPositionTemp[AllPossibleMills[n, 0, 0], AllPossibleMills[n, 0, 1]] &&
                    playerPositionTemp[AllPossibleMills[n, 1, 0], AllPossibleMills[n, 1, 1]] &&
                    playerPositionTemp[AllPossibleMills[n, 2, 0], AllPossibleMills[n, 2, 1]]))
                {
                    return true;
                }
            }

            return false;
        }

        static public string CheckName(string name, int index)
        {
            while (name == string.Empty || name.Length < 2 || name.Length > MaxNameLenght)
            {
                if (index % 2 == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.SetCursorPosition(WindowWidth / 2 - 10, WindowHeight / 2);
                Console.Write("Player {0} Name: ", index);
                name = Console.ReadLine();
                Console.Clear();
            }

            return name;
        }

        static List<String> InputPlayersNames(int numberOfPlayers)
        {
            List<string> playrsList = new List<string>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Clear();
                Console.SetCursorPosition(WindowWidth / 2 - 10, WindowHeight / 2);
                if (i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write("Player {0} Name: ", i + 1);
                string name = CheckName(Console.ReadLine(), i + 1);
                playrsList.Add(name);
            }

            Console.Clear();
            return playrsList;
        }

        // Reads string with given length
        private static string ReadString(byte limit)
        {
            string str = string.Empty;
            while (true)
            {
                char c = Console.ReadKey(true).KeyChar;
                if (c == '\r')
                {
                    break;
                }

                if (c == '\b')
                {
                    if (str != string.Empty)
                    {
                        str = str.Substring(0, str.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (str.Length < limit)
                {
                    // limit of digits
                    Console.Write(c);
                    str += c;
                }
            }

            CheckInputFormat(ref str);
            return str.ToUpper();
        }

        // Reverse the input if it is necessary
        private static void CheckInputFormat(ref string input)
        {
            if (input.Length == 2)
            {
                char firstSymbol = input[0];
                if (!char.IsDigit(firstSymbol))
                {
                    input = input[1].ToString() + firstSymbol;
                }
            }
            else if (input.Length == 4)
            {
                if (!char.IsDigit(input[0]))
                {
                    StringBuilder buf = new StringBuilder(input.Length);
                    buf.Append(input[1]);
                    buf.Append(input[0]);
                    buf.Append(input[2]);
                    buf.Append(input[3]);
                    input = buf.ToString();
                }

                if (!char.IsDigit(input[2]))
                {
                    StringBuilder buf = new StringBuilder(input.Length);
                    buf.Append(input[0]);
                    buf.Append(input[1]);
                    buf.Append(input[3]);
                    buf.Append(input[2]);
                    input = buf.ToString();
                }
            }
        }
    }
}