namespace TheMillGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Robot : Player
    {
        // List of all possible input coordinates
        private static string[,] coords = { 
                {"7A", "7D", "7G","4A", "4G", "1A","1D", "1G"},
                {"6B", "6D", "6F","4B", "4F", "2B","2D", "2F"},
                {"5C", "5D", "5E","4C", "4E", "3C","3D", "3E"}
                                          };

        private static Random generator;

        // Constructor
        public Robot(Piece robotPieces, string robotName = "PC")
            : base(robotPieces, robotName)
        {
            generator = new Random();
        }

        // Generate input string to place some piece on the board
        public string GenerateInputForPlacing(byte[, ,] allMills, Piece player)
        {
            #region Check for free mill
            for (int n = 0; n < allMills.GetLength(0); n++)
            {
                bool[] pieces = new bool[3];
                for (int j = 0; j < allMills.GetLength(1); j++)
                {
                    if (this.piece.positionsOnBoard[allMills[n, j, 0], allMills[n, j, 1]])
                    {
                        pieces[j] = true;
                    }
                }

                if (pieces.Where(m => m == true).Count() == 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!pieces[i] &&
                            !this.piece.positionsOnBoard[allMills[n, i, 0], allMills[n, i, 1]] &&
                            !player.positionsOnBoard[allMills[n, i, 0], allMills[n, i, 1]])
                        {
                            return coords[allMills[n, i, 0], allMills[n, i, 1]];
                        }
                    }
                }
            }
            #endregion

            #region  If the number of occupied pieces for mill is 2
            for (int n = 0; n < allMills.GetLength(0); n++)
            {
                bool[] pieces = new bool[3];
                for (int j = 0; j < allMills.GetLength(1); j++)
                {
                    if (player.positionsOnBoard[allMills[n, j, 0], allMills[n, j, 1]])
                    {
                        pieces[j] = true;
                    }
                }

                if (pieces.Where(m => m == true).Count() == 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!pieces[i] && !this.piece.positionsOnBoard[allMills[n, i, 0], allMills[n, i, 1]])
                        {
                            return coords[allMills[n, i, 0], allMills[n, i, 1]];
                        }
                    }
                }

            }
            #endregion

            #region  If the number of occupied pieces for mill is 1
            for (int n = 0; n < allMills.GetLength(0); n++)
            {
                bool[] pieces = new bool[3];
                for (int j = 0; j < allMills.GetLength(1); j++)
                {
                    if (player.positionsOnBoard[allMills[n, j, 0], allMills[n, j, 1]])
                    {
                        pieces[j] = true;
                    }
                }

                if (pieces.Where(m => m == true).Count() == 1)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (pieces[i] && !this.piece.positionsOnBoard[allMills[n, 2, 0], allMills[n, 2, 1]])
                                {
                                    return coords[allMills[n, 2, 0], allMills[n, 2, 1]];
                                }
                                break;
                            case 1:
                                int gen = (generator.Next(0, 2) == 0) ? 0 : 2;
                                if (pieces[i] && !this.piece.positionsOnBoard[allMills[n, gen, 0], allMills[n, gen, 1]])
                                {
                                    return coords[allMills[n, gen, 0], allMills[n, gen, 1]];
                                }
                                break;
                            case 2:
                                if (pieces[i] && !this.piece.positionsOnBoard[allMills[n, 0, 0], allMills[n, 0, 1]])
                                {
                                    return coords[allMills[n, 0, 0], allMills[n, 0, 1]];
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
            #endregion

            // If there aren't any opponent pieces on the bord
            return coords[generator.Next(0, 3), generator.Next(0, 8)];
        }

        // Generate input string to move some piece
        public string GenerateInputForMoving(Piece player)
        {
            List<int[]> pieces = new List<int[]>();

            for (int i = 0; i < this.piece.positionsOnBoard.GetLength(0); i++)
            {
                for (int j = 0; j < this.piece.positionsOnBoard.GetLength(1); j++)
                {
                    if (this.piece.positionsOnBoard[i, j])
                    {
                        pieces.Add(new int[] { i, j });
                    }
                }
            }

            while (true)
            {
                int generatePiece = generator.Next(0, pieces.Count);
                int gen = 0;
                bool checkRectangleCorners = true;
                bool checkBorderRectangles = true;
                bool checkMiddleRectangle = true;

                #region Check all rectangle corners
                switch (int.Parse(pieces[generatePiece][0].ToString() + pieces[generatePiece][1].ToString()))
                {
                    case 0:
                    case 10:
                    case 20:
                        gen = (generator.Next(0, 2) == 0) ? 1 : 3;
                        break;
                    case 2:
                    case 12:
                    case 22:
                        gen = (generator.Next(0, 2) == 0) ? 1 : 4;
                        break;
                    case 5:
                    case 15:
                    case 25:
                        gen = (generator.Next(0, 2) == 0) ? 3 : 6;
                        break;
                    case 7:
                    case 17:
                    case 27:
                        gen = (generator.Next(0, 2) == 0) ? 4 : 6;
                        break;
                    default:
                        checkRectangleCorners = false;
                        break;
                }
                if (checkRectangleCorners)
                {
                    if (!player.positionsOnBoard[0, gen] && !this.piece.positionsOnBoard[0, gen])
                    {
                        return coords[pieces[generatePiece][0], pieces[generatePiece][1]] + coords[pieces[generatePiece][0], gen];
                    }
                }
                #endregion

                #region Check all middle pieces on border rectangles
                switch (int.Parse(pieces[generatePiece][0].ToString() + pieces[generatePiece][1].ToString()))
                {
                    case 1:
                    case 21:
                        gen = (generator.Next(0, 2) == 0) ? 0 : 2;
                        break;
                    case 3:
                    case 23:
                        gen = (generator.Next(0, 2) == 0) ? 0 : 5;
                        break;
                    case 4:
                    case 24:
                        gen = (generator.Next(0, 2) == 0) ? 2 : 7;
                        break;
                    case 6:
                    case 26:
                        gen = (generator.Next(0, 2) == 0) ? 5 : 7;
                        break;
                    default:
                        checkBorderRectangles = false;
                        break;
                }
                if (checkBorderRectangles)
                {
                    if (!player.positionsOnBoard[0, gen] && !this.piece.positionsOnBoard[0, gen])
                    {
                        return coords[pieces[generatePiece][0], pieces[generatePiece][1]] + coords[pieces[generatePiece][0], gen];
                    }
                    if (!player.positionsOnBoard[1, pieces[generatePiece][1]] && !this.piece.positionsOnBoard[1, pieces[generatePiece][1]])
                    {
                        return coords[pieces[generatePiece][0], pieces[generatePiece][1]] + coords[1, pieces[generatePiece][1]];
                    }
                }
                #endregion

                #region Check all middle pieces on middle rectangle
                switch (int.Parse(pieces[generatePiece][0].ToString() + pieces[generatePiece][1].ToString()))
                {
                    case 11:
                        gen = (generator.Next(0, 2) == 0) ? 0 : 2;
                        break;
                    case 13:
                        gen = (generator.Next(0, 2) == 0) ? 0 : 5;
                        break;
                    case 14:
                        gen = (generator.Next(0, 2) == 0) ? 2 : 7;
                        break;
                    case 16:
                        gen = (generator.Next(0, 2) == 0) ? 5 : 7;
                        break;
                    default:
                        checkMiddleRectangle = false;
                        break;
                }
                if (checkMiddleRectangle)
                {
                    if (!player.positionsOnBoard[0, gen] && !this.piece.positionsOnBoard[0, gen])
                    {
                        return coords[pieces[generatePiece][0], pieces[generatePiece][1]] + coords[pieces[generatePiece][0], gen];
                    }

                    if (!player.positionsOnBoard[0, pieces[generatePiece][1]] && !this.piece.positionsOnBoard[0, pieces[generatePiece][1]])
                    {
                        return coords[pieces[generatePiece][0], pieces[generatePiece][1]] + coords[0, pieces[generatePiece][1]];
                    }

                    if (!player.positionsOnBoard[2, pieces[generatePiece][1]] && !this.piece.positionsOnBoard[2, pieces[generatePiece][1]])
                    {
                        return coords[pieces[generatePiece][0], pieces[generatePiece][1]] + coords[2, pieces[generatePiece][1]];
                    }
                }
                #endregion
            }
        }

        // Generate input string to remove some piece from the board
        internal string GenerateInputForRemoving(byte[, ,] allMills, Piece player)
        {
            for (int n = 0; n < allMills.GetLength(0); n++)
            {
                for (int j = 0; j < allMills.GetLength(1); j++)
                {
                    if (player.positionsOnBoard[allMills[n, j, 0], allMills[n, j, 1]])
                    {
                        return coords[allMills[n, j, 0], allMills[n, j, 1]];
                    }
                }
            }
            throw new Exception("There is not piece for removig!");
        }
    }
}
