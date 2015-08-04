namespace TheMillGame
{
    using System;

    public class Player : IPlayer
    {
        private string name;

        #region Properties
        public Piece piece { get; set; }
        public Position Display { get; set; }
        public ConsoleColor Color { get; set; }
        public string Name
        {
            set
            {
                if (value != null)
                {
                    name = value;
                }
                else
                {
                    throw new ArgumentNullException("The name of the player can't be \'null\'!");
                }
            }

            get
            {
                return name;
            }
        }
        #endregion

        // Constructor
        public Player(Piece piece, string name)
        {
            this.piece = piece;
            this.Name = name;
        }

        public bool Loose(bool[,] op)
        {
            int broqch = 0;

            if (piece.Count == 2)
            {
                return true;
            }
            else
            {
                // 0.0 1.0 2.0
                for (int i = 0; i < piece.positionsOnBoard.GetLength(0); i++)
                {
                    if ((piece.positionsOnBoard[i, 0]) &&
                        (piece.positionsOnBoard[i, 1] || op[i, 1]) &&
                        (piece.positionsOnBoard[i, 3] || op[i, 3]))
                    {
                        broqch++;
                    }
                }

                // 0.2 1.2 2.2
                for (int i = 0; i < piece.positionsOnBoard.GetLength(0); i++)
                {
                    if ((piece.positionsOnBoard[i, 2]) &&
                        (piece.positionsOnBoard[i, 1] || op[i, 1]) &&
                        (piece.positionsOnBoard[i, 4] || op[i, 4]))
                    {
                        broqch++;
                    }
                }

                // 0.7 1.7 2.7
                for (int i = 0; i < piece.positionsOnBoard.GetLength(0); i++)
                {
                    if ((piece.positionsOnBoard[i, 7]) &&
                        (piece.positionsOnBoard[i, 4] || op[i, 4]) &&
                        (piece.positionsOnBoard[i, 6] || op[i, 6]))
                    {
                        broqch++;
                    }
                }

                // 0.5 1.5 2.5
                for (int i = 0; i < piece.positionsOnBoard.GetLength(0); i++)
                {
                    if ((piece.positionsOnBoard[i, 5]) &&
                        (piece.positionsOnBoard[i, 3] || op[i, 3]) &&
                        (piece.positionsOnBoard[i, 6] || op[i, 6]))
                    {
                        broqch++;
                    }
                }

                // 0.1 0.6
                for (int i = 1; i < 7; i += 5)
                {
                    if ((piece.positionsOnBoard[0, i]) &&
                        (piece.positionsOnBoard[0, i + 1] || op[0, i + 1]) &&
                        (piece.positionsOnBoard[0, i - 1] || op[0, i - 1]) &&
                        (piece.positionsOnBoard[1, i] || op[1, i]))
                    {
                        broqch++;
                    }
                }

                // 1.1 1.6
                for (int i = 1; i < 7; i += 5)
                {
                    if ((piece.positionsOnBoard[1, i]) &&
                        (piece.positionsOnBoard[1, i + 1] || op[1, i + 1]) &&
                        (piece.positionsOnBoard[1, i - 1] || op[1, i - 1]) &&
                        (piece.positionsOnBoard[0, i] || op[0, i]) &&
                        (piece.positionsOnBoard[2, i] || op[2, i]))
                    {
                        broqch++;
                    }
                }

                // 2.1 2.6
                for (int i = 1; i < 7; i += 5)
                {
                    if ((piece.positionsOnBoard[2, i]) &&
                        (piece.positionsOnBoard[2, i + 1] || op[2, i + 1]) &&
                        (piece.positionsOnBoard[2, i - 1] || op[2, i - 1]) &&
                        (piece.positionsOnBoard[1, i] || op[1, i]))
                    {
                        broqch++;
                    }
                }

                // 1.3 
                if (piece.positionsOnBoard[1, 3])
                {
                    if ((piece.positionsOnBoard[1, 0] || op[1, 0]) &&
                        (piece.positionsOnBoard[1, 5] || op[1, 5]) &&
                        (piece.positionsOnBoard[0, 3] || op[0, 3]) &&
                        (piece.positionsOnBoard[2, 3] || op[2, 3]))
                    {
                        broqch++;
                    }
                }

                // 1.4 
                if (piece.positionsOnBoard[1, 4])
                {
                    if ((piece.positionsOnBoard[1, 2] || op[1, 2]) &&
                        (piece.positionsOnBoard[1, 7] || op[1, 7]) &&
                        (piece.positionsOnBoard[0, 4] || op[0, 4]) &&
                        (piece.positionsOnBoard[2, 4] || op[2, 4]))
                    {
                        broqch++;
                    }
                }

                // 0.4 2.4 0.3 2.3
                for (int i = 0; i < 3; i += 2)
                {
                    for (int j = 3; j < 5; j++)
                    {
                        if (piece.positionsOnBoard[i, j] && (piece.positionsOnBoard[1, j] || op[1, j]))
                        {
                            if ((j == 3) &&
                                (piece.positionsOnBoard[i, 0] || op[i, 0]) &&
                                (piece.positionsOnBoard[i, 5] || op[i, 5]))
                            {
                                broqch++;
                            }
                            else if ((j == 4) &&
                                (piece.positionsOnBoard[i, 2] || op[i, 2]) &&
                                (piece.positionsOnBoard[i, 7] || op[i, 7]))
                            {
                                broqch++;
                            }
                        }
                    }
                }
            }

            if (broqch == piece.Count)
            {
                return true;
            }

            return false;
        }
    }
}
