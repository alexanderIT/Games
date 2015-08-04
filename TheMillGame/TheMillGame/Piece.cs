namespace TheMillGame
{
    using System;

    public class Piece
    {
        private const string PieceBody = "███";
        private ConsoleColor piecesColor;
        public Position[,] positions;
        public bool[,] positionsOnBoard;
        public Position curPos; // used like buffer
        private int count;

        #region Properties
        public int Count
        {
            set
            {
                if (value >= 2 && value <= 9)
                {
                    count = value;
                }
                else
                {
                    // Броят на пуловете не може да е по-малко от 2 и по-голям от 9
                    throw new ArgumentOutOfRangeException(
                        "The number of pieces is not less than 2 and greater than 9!");
                }
            }
            get { return count; }
        }

        public ConsoleColor PiecesColor
        {
            set
            {
                piecesColor = value;
            }

            get
            {
                return piecesColor;
            }
        }
        #endregion

        // Constructor
        public Piece()
        {
            piecesColor = ConsoleColor.Green;
            count = 9;
            positions = null;
        }

        // Move the pieces
        public void Move(string input, Piece opponent, Board board)
        {
            Position fromPos = GetPositionFromInput(input[0].ToString() + input[1].ToString());
            Position toPos = GetPositionFromInput(input[2].ToString() + input[3].ToString());

            if (CanBeMoved(fromPos, toPos, opponent))
            {
                positionsOnBoard[fromPos.x, fromPos.y] = false; // освобождаване на старата позиция
                DrawPieceAt(fromPos, board.PiecePlaceColor, opponent);

                DrawPieceAt(toPos, PiecesColor, opponent);
                positionsOnBoard[toPos.x, toPos.y] = true; // поставяне на новата позиция 
            }
            else
            {
                throw new ArgumentException("Please try again!");
            }
        }

        // Check is the piece movable
        private bool CanBeMoved(Position fromPos, Position toPos, Piece opponent)
        {
            if ((fromPos.x != toPos.x || fromPos.y != toPos.y) && positionsOnBoard[fromPos.x, fromPos.y])
            {
                bool result = false;
                if (fromPos.x == toPos.x)
                {
                    switch (fromPos.y)
                    {
                        case 0:
                            if (toPos.y == fromPos.y + 1 &&
                                !positionsOnBoard[fromPos.x, fromPos.y + 1] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y + 1])
                            {
                                result = true;
                            }
                            else if (toPos.y == fromPos.y + 3 &&
                                !positionsOnBoard[fromPos.x, fromPos.y + 3] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y + 3])
                            {
                                result = true;
                            }
                            break;
                        case 1:
                        case 6:
                            if (toPos.y == fromPos.y + 1 &&
                                !positionsOnBoard[fromPos.x, fromPos.y + 1] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y + 1])
                            {
                                result = true;
                            }
                            else if (toPos.y == fromPos.y - 1 &&
                                !positionsOnBoard[fromPos.x, fromPos.y - 1] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y - 1])
                            {
                                result = true;
                            }
                            break;
                        case 2:
                            if (toPos.y == fromPos.y - 1 &&
                                !positionsOnBoard[fromPos.x, fromPos.y - 1] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y - 1])
                            {
                                result = true;
                            }
                            else if (toPos.y == fromPos.y + 2 &&
                                !positionsOnBoard[fromPos.x, fromPos.y + 2] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y + 2])
                            {
                                result = true;
                            }
                            break;
                        case 3:
                            if (toPos.y == fromPos.y - 3 &&
                                !positionsOnBoard[fromPos.x, fromPos.y - 3] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y - 3])
                            {
                                result = true;
                            }
                            else if (toPos.y == fromPos.y + 2 &&
                                !positionsOnBoard[fromPos.x, fromPos.y + 2] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y + 2])
                            {
                                result = true;
                            }
                            break;
                        case 4:
                            if (toPos.y == fromPos.y - 2 &&
                                !positionsOnBoard[fromPos.x, fromPos.y - 2] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y - 2])
                            {
                                result = true;
                            }
                            else if (toPos.y == fromPos.y + 3 &&
                                !positionsOnBoard[fromPos.x, fromPos.y + 3] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y + 3])
                            {
                                result = true;
                            }
                            break;
                        case 5:
                            if (toPos.y == fromPos.y - 2 &&
                                !positionsOnBoard[fromPos.x, fromPos.y - 2] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y - 2])
                            {
                                result = true;
                            }
                            else if (toPos.y == fromPos.y + 1 &&
                                !positionsOnBoard[fromPos.x, fromPos.y + 1] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y + 1])
                            {
                                result = true;
                            }
                            break;
                        case 7:
                            if (toPos.y == fromPos.y - 1 &&
                                !positionsOnBoard[fromPos.x, fromPos.y - 1] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y - 1])
                            {
                                result = true;
                            }
                            else if (toPos.y == fromPos.y - 3 &&
                                !positionsOnBoard[fromPos.x, fromPos.y - 3] &&
                                !opponent.positionsOnBoard[fromPos.x, fromPos.y - 3])
                            {
                                result = true;
                            }
                            break;
                        default:
                            result = false;
                            break;
                    }

                    if (result)
                    {
                        return true;
                    }
                }
                else
                {
                    if (fromPos.y == 3 || fromPos.y == 4 || fromPos.y == 1 || fromPos.y == 6)
                    {
                        switch (fromPos.x)
                        {
                            case 0:
                                if (toPos.x == fromPos.x + 1 &&
                                    !positionsOnBoard[fromPos.x + 1, fromPos.y] &&
                                    !opponent.positionsOnBoard[fromPos.x + 1, fromPos.y])
                                {
                                    result = true;
                                }
                                break;
                            case 1:
                                if (toPos.x == fromPos.x + 1 &&
                                    !positionsOnBoard[fromPos.x + 1, fromPos.y] &&
                                    !opponent.positionsOnBoard[fromPos.x + 1, fromPos.y])
                                {
                                    result = true;
                                }
                                else if (toPos.x == fromPos.x - 1 &&
                                    !positionsOnBoard[fromPos.x - 1, fromPos.y] &&
                                    !opponent.positionsOnBoard[fromPos.x - 1, fromPos.y])
                                {
                                    result = true;
                                }
                                break;
                            case 2:
                                if (toPos.x == fromPos.x - 1 &&
                                    !positionsOnBoard[fromPos.x - 1, fromPos.y] &&
                                    !opponent.positionsOnBoard[fromPos.x - 1, fromPos.y])
                                {
                                    result = true;
                                }
                                break;
                            default:
                                result = false;
                                break;
                        }

                        if (result)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        // Draw some piece with color and position
        public void DrawPieceAt(Position pos, ConsoleColor color, Piece opponent)
        {
            if (!positionsOnBoard[pos.x, pos.y] && !opponent.positionsOnBoard[pos.x, pos.y])
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(positions[pos.x, pos.y].x - (PieceBody.Length / 2),
                    positions[pos.x, pos.y].y);
                Console.Write(PieceBody);
            }
            else
            {
                throw new ArgumentException("Please try again!");
            }
        }

        // Load piece coordinates on the board
        public void LoadPiecesPositionsFromBoard(Board board)
        {
            int rectangleCount = board.positions.GetLength(0);
            int pointsCountInRect = board.positions.GetLength(1);

            positions = new Position[rectangleCount, pointsCountInRect];
            positionsOnBoard = new bool[rectangleCount, pointsCountInRect];

            for (int i = 0; i < rectangleCount; i++)
            {
                for (int j = 0; j < pointsCountInRect; j++)
                {
                    positions[i, j].x = board.positions[i, j].x;
                    positions[i, j].y = board.positions[i, j].y;
                }
            }
        }

        // Take the position of the piece from the input string
        public Position GetPositionFromInput(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                // can be added instruction for correct input
                throw new ArgumentException("Invalid input!");
            }

            Position result = new Position();
            result.x = DetermineRectangle(input);
            result.y = DeterminePointFromRect(input);
            Console.SetCursorPosition(0, 39);
            return result;
        }

        // Define the number of rectangle (the 1st index - the row)
        private int DetermineRectangle(string input)
        {
            if (input[0] == '7' || input[0] == '1' || (input[0] == '4' && (input[1] == 'A' || input[1] == 'G')))
            {
                return 0;
            }
            else if (input[0] == '6' || input[0] == '2' || (input[0] == '4' && (input[1] == 'B' || input[1] == 'F')))
            {
                return 1;
            }
            else if (input[0] == '5' || input[0] == '3' || (input[0] == '4' && (input[1] == 'C' || input[1] == 'E')))
            {
                return 2;
            }
            else
            {
                throw new ArgumentException("Invalid character!");
            }
        }

        // Define the number of point in rectangle (the 2nd index - the column)
        private int DeterminePointFromRect(string input)
        {
            int result = -1;
            switch (input[0])
            {
                case '7':
                    if (input[1] == 'A') { result = 0; }
                    else if (input[1] == 'D') { result = 1; }
                    else if (input[1] == 'G') { result = 2; }
                    break;
                case '6':
                    if (input[1] == 'B') { result = 0; }
                    else if (input[1] == 'D') { result = 1; }
                    else if (input[1] == 'F') { result = 2; }
                    break;
                case '5':
                    if (input[1] == 'C') { result = 0; }
                    else if (input[1] == 'D') { result = 1; }
                    else if (input[1] == 'E') { result = 2; }
                    break;
                case '4':
                    if (input[1] == 'A' || input[1] == 'B' || input[1] == 'C')
                    {
                        result = 3;
                    }
                    else if (input[1] == 'E' || input[1] == 'F' || input[1] == 'G')
                    {
                        result = 4;
                    }
                    break;
                case '3':
                    if (input[1] == 'C') { result = 5; }
                    else if (input[1] == 'D') { result = 6; }
                    else if (input[1] == 'E') { result = 7; }
                    break;
                case '2':
                    if (input[1] == 'B') { result = 5; }
                    else if (input[1] == 'D') { result = 6; }
                    else if (input[1] == 'F') { result = 7; }
                    break;
                case '1':
                    if (input[1] == 'A') { result = 5; }
                    else if (input[1] == 'D') { result = 6; }
                    else if (input[1] == 'G') { result = 7; }
                    break;
                default:
                    result = -1;
                    break;
            }

            if (result == -1)
            {
                throw new ArgumentException("Invalid input!");
            }

            return result;
        }

        // Remove some piece from the board
        public void RemovePiece(Position remove, Piece opponent)
        {
            if (positionsOnBoard[remove.x, remove.y])
            {
                this.count--;
                positionsOnBoard[remove.x, remove.y] = false;
                DrawPieceAt(remove, ConsoleColor.Gray, opponent);
            }
            else
            {
                throw new Exception("Piece is missing on this position!");
            }
        }

        // Calculates the number of placed pieces
        public int GetPiecesPlacedOnBoard()
        {
            int piecesPlacedOnBoard = 0;
            for (int i = 0; i < positionsOnBoard.GetLength(0); i++)
            {
                for (int j = 0; j < positionsOnBoard.GetLength(1); j++)
                {
                    if (positionsOnBoard[i, j])
                    {
                        piecesPlacedOnBoard++;
                    }
                }
            }
            return piecesPlacedOnBoard;
        }
    }
}