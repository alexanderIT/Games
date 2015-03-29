namespace BattleshipGame
{
    using System;
    using System.Threading;
    using BattleshipGame.Contracts;
    public class GameBoard : IGameBoard
    {

        const char EMPTY = '.';
        const int DELIMITERSIZE_ROWCOL = 1;
        const string HIT = "HIT";
        const string MISS = "MISS";
        const char HITSHIP = 'X';
        const char MISSSHIP = '_';
        const char SHIP = 'S';
        const char EMPTYFIELD = ' ';
        const string EMPTYSTRING = " ";

        /// <summary>
        /// Char array with ships which GenarateRandomBattleShip class generate for the game.
        /// </summary>
        public char[,] GameBoardCPUField { get; set; }
        /// <summary>
        /// Char array with hitted ships in the current game.
        /// </summary>
        public char[,] GameBoardPlayerField { get; set; }
        /// <summary>
        /// Check is all ships in the game is destroyed.
        /// </summary>
        public bool GameComplete { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Shots { get; set; }


        bool hit = false;
        bool miss = false;
        public GameBoard()
        {
            this.GameBoardCPUField = new char[10, 10];
            this.GameBoardPlayerField = new char[10, 10];
        }

        public GameBoard(int boardSizeX, int boardSizeY)
        {
            this.GameBoardCPUField = new char[boardSizeX, boardSizeY];
            this.GameBoardPlayerField = new char[boardSizeX, boardSizeY];
        }
        /// <summary>
        /// Fill two char array GameBoardCPUField and GameBoardPlayerField with no shot symbol ".".
        /// </summary>
        public void EmptyGameBoardField()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    GameBoardCPUField[col, row] = EMPTY;
                    GameBoardPlayerField[col, row] = EMPTY;
                }
            }
        }

        /// <summary>
        /// Set GameComplete in true if all ships in the game is destroyed.
        /// </summary>
        /// <param name="board">Current board with hitted ship in game</param>
        public void ChechForHits(char[,] board)
        {
            int counter = 0;
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (board[row, col] == HITSHIP)
                    {
                        counter++;                       
                    }
                }
            }
            if (counter==13)
            {
                this.GameComplete = true;
            }
            else
            {
                this.GameComplete = false;
                counter = 0;
            }
        }

        /// <summary>
        /// Print current state of the game in console.
        /// </summary>
        /// <param name="GameBoardCPUField">Char array with genereted random ships.</param>
        /// <param name="GameBoardPlayerField">Char array which the PrintBoard method use to fill with hitted and missed ships in current game. </param>
        /// <param name="x">Current choise for X coordinate return from ReadAndValidateInput method.</param>
        /// <param name="y">Current choise for Y coordinate return from ReadAndValidateInput method.</param>
        public void PrintBoard(char[,] GameBoardCPUField, char[,] GameBoardPlayerField, int x, int y)
        {

            Console.Clear();
            Thread.Sleep(50);
            if (miss)
            {
                Console.WriteLine(MISS);
                miss = false;
            }
            if (hit)
            {
                Console.WriteLine(HIT);
                hit = false;
            }

            Console.WriteLine();
            string strBoardDelimiter_Cols = new string(EMPTYFIELD, DELIMITERSIZE_ROWCOL);
            string strBoardDelimiter_Rows = EMPTYSTRING;

            int intRowNumberMaxCharLength = (GameBoardPlayerField.GetUpperBound(0) + 1).ToString().Length;
            Console.Write(strBoardDelimiter_Rows);

            for (int intCol = 0; intCol <= GameBoardPlayerField.GetUpperBound(0); intCol++)
            {
                Console.Write(intCol.ToString());
            }
            Console.WriteLine(strBoardDelimiter_Rows);


            for (int intRow = 0; intRow <= GameBoardPlayerField.GetUpperBound(1); intRow++)
            {

                Console.Write((char)(65 + intRow));

                for (int intCol = GameBoardPlayerField.GetLowerBound(1); intCol <= GameBoardPlayerField.GetUpperBound(1); intCol++)
                {

                    if (GameBoardCPUField[y, x] == SHIP || GameBoardCPUField[y, x] == HITSHIP)
                    {
                        this.GameBoardPlayerField[y, x] = HITSHIP;                       
                        this.hit = true;
                        

                    }
                    else if (GameBoardCPUField[y, x] == EMPTYFIELD || GameBoardCPUField[y, x] == EMPTY)
                    {
                        this.GameBoardPlayerField[y, x] = MISSSHIP;
                        this.miss = true;
                    }
   
                    Console.Write(GameBoardPlayerField[intRow, intCol]);
                }
                Console.WriteLine(strBoardDelimiter_Rows);
            }  
          
            this.Shots++;
            this.ChechForHits(GameBoardPlayerField);
        }

        /// <summary>
        /// Print coordinates of the generated ships if "show" command occurred in console.
        /// </summary>
        /// <param name="GameBoardCPUField">Field of char array with generated random ships.</param>
        public void PrintBoardWhitShips(char[,] GameBoardCPUField)
        {
            Console.Clear();

            string strBoardDelimiter_Cols = new string(' ', DELIMITERSIZE_ROWCOL);
            string strBoardDelimiter_Rows = " ";

            int intRowNumberMaxCharLength = (GameBoardCPUField.GetUpperBound(0) + 1).ToString().Length;
            Console.Write(strBoardDelimiter_Rows);

            for (int intCol = 0; intCol <= GameBoardCPUField.GetUpperBound(0); intCol++)
            {
                Console.Write(intCol.ToString());
            }
            Console.WriteLine(strBoardDelimiter_Rows);

            for (int intRow = 0; intRow <= GameBoardCPUField.GetUpperBound(1); intRow++)
            {

                Console.Write((char)(65 + intRow));

                for (int intCol = GameBoardCPUField.GetLowerBound(1); intCol <= GameBoardCPUField.GetUpperBound(1); intCol++)
                {
                    if (GameBoardCPUField[intRow, intCol] == EMPTY || GameBoardCPUField[intRow, intCol] == EMPTYFIELD)
                    {
                        this.GameBoardCPUField[intRow, intCol] = EMPTYFIELD;
                    }
                    else
                    {
                        this.GameBoardCPUField[intRow, intCol] = HITSHIP;
                    }
                    Console.Write(GameBoardCPUField[intRow, intCol]);
                }

                Console.WriteLine(strBoardDelimiter_Rows);
            }

        }
    }
}
