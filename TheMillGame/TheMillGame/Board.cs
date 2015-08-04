namespace TheMillGame
{
    using System;
    using System.Collections.Generic;

    public class Board
    {
        #region Constants
        private const string PiecePlace = "███";       // █ \u2588
        private const char HorizontalLine = '\u2500';   // ─
        private const char VerticalLine = '\u2502';     // │ 
        private const char SymbolPiece = '\u25A0';

        private const int marginLeft = 10;
        private const int marginTop = 3;
        private const int numOfHorizontalLines = 6;
        #endregion

        #region Fields
        private int width;
        private int height;
        private ConsoleColor backColor;
        private ConsoleColor borderColor;
        private ConsoleColor piecePlaceColor;
        private ConsoleColor textColor;
        private ConsoleColor textBackColor;
        public Position[,] positions;
        private int spacer;
        #endregion

        #region Properties
        public int Width
        {
            set
            {
                if (value > 0)
                {
                    this.width = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The width can't be negative or zero!");
                }
            }
            get { return this.width; }
        }

        public int Height
        {
            set
            {
                if (value > 0)
                {
                    this.height = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The height can't be negative or zero!");
                }
            }
            get { return this.height; }
        }

        public ConsoleColor BackgroundColor
        {
            set { this.backColor = value; }
            get { return this.backColor; }
        }

        public ConsoleColor BorderColor
        {
            set { this.borderColor = value; }
            get { return this.borderColor; }
        }

        public ConsoleColor PiecePlaceColor
        {
            set { piecePlaceColor = value; }
            get { return piecePlaceColor; }
        }

        public ConsoleColor TextColor
        {
            set { textColor = value; }
            get { return textColor; }
        }
        #endregion

        public Board()
        {
            backColor = Console.BackgroundColor;
            borderColor = ConsoleColor.Cyan;
            piecePlaceColor = ConsoleColor.Gray;
            textColor = ConsoleColor.Black;
            textBackColor = ConsoleColor.Cyan;
            width = Console.WindowWidth;
            height = Console.WindowHeight;
            spacer = Console.WindowHeight - marginLeft;
            positions = new Position[3, 8];
        }

        // Draw the board
        public void DrawBoard()
        {
            int left, right, top;
            int numberOfChars;
            int length = numOfHorizontalLines >> 1;
            int col;

            for (int i = 0; i < length; i++, spacer -= 8)
            {
                col = 0;
                left = marginLeft + i * 10;
                top = marginTop + (i << 2);
                right = Console.BufferWidth - left;
                numberOfChars = Console.BufferWidth - 2 * left;

                #region Draw Text
                /* Draw numbers */
                DrawText((numOfHorizontalLines - i + 1).ToString(), marginLeft / 2, top);
                DrawText((length + 1).ToString(), marginLeft / 2, top + spacer / 2);
                DrawText((length + i - 2).ToString(), marginLeft / 2, top + spacer);

                /* Draw letters */
                DrawText(((char)(65 + i)).ToString(), left, Console.WindowHeight - marginLeft / 2);
                DrawText(((char)(65 + length)).ToString(), left + numberOfChars / 2, Console.WindowHeight - marginLeft / 2);
                DrawText(((char)(65 + length + (length - i))).ToString(), right, Console.WindowHeight - marginLeft / 2);
                #endregion

                #region Draw Horizontal Lines
                DrawHorizontalLine(left, top, numberOfChars);
                if (i < length - 1)
                {
                    DrawHorizontalLine(left, top + spacer / 2, marginLeft);
                    DrawHorizontalLine(right - marginLeft, top + spacer / 2, marginLeft);
                }
                DrawHorizontalLine(left, top + spacer, numberOfChars);
                #endregion

                #region Draw Vertical Lines
                for (int j = 1; j < spacer; j++)
                {
                    DrawVerticalLine(left, top + j);
                    DrawVerticalLine(right, top + j);
                    if (j < marginTop + 1)
                    {
                        // при промяна на margin'а може да не работи както трябва!
                        if (i < 2)
                        {
                            DrawVerticalLine(left + numberOfChars / 2, top + j);
                            DrawVerticalLine(left + numberOfChars / 2, top + spacer - marginTop - 1 + j);
                        }
                    }
                }
                #endregion

                #region Draw Piece Places
                DrawPiecePlace(left, top, piecePlaceColor, PiecePlace);
                DrawPiecePlace(left + numberOfChars / 2, top, piecePlaceColor, PiecePlace);
                DrawPiecePlace(right, top, piecePlaceColor, PiecePlace);

                DrawPiecePlace(left, top + spacer / 2, piecePlaceColor, PiecePlace);
                DrawPiecePlace(right, top + spacer / 2, piecePlaceColor, PiecePlace);

                DrawPiecePlace(left, top + spacer, piecePlaceColor, PiecePlace);
                DrawPiecePlace(left + numberOfChars / 2, top + spacer, piecePlaceColor, PiecePlace);
                DrawPiecePlace(right, top + spacer, piecePlaceColor, PiecePlace);

                #region Array with positions (like rectangle)
                positions[i, col].x = left;
                positions[i, col++].y = top;

                positions[i, col].x = left + numberOfChars / 2;
                positions[i, col++].y = top;

                positions[i, col].x = right;
                positions[i, col++].y = top;

                positions[i, col].x = left;
                positions[i, col++].y = top + spacer / 2;
                positions[i, col].x = right;
                positions[i, col++].y = top + spacer / 2;

                positions[i, col].x = left;
                positions[i, col++].y = top + spacer;
                positions[i, col].x = left + numberOfChars / 2;
                positions[i, col++].y = top + spacer;
                positions[i, col].x = right;
                positions[i, col++].y = top + spacer;
                #endregion

                #endregion
            }
        }

        // Draw the place for some piece
        public void DrawPiecePlace(int left, int top, ConsoleColor color, string symbol)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left - (PiecePlace.Length / 2), top);
            Console.Write(symbol);
        }

        // Draw an horizontal line
        public void DrawHorizontalLine(int left, int top, int numberOfChars)
        {
            Console.ForegroundColor = borderColor;
            Console.SetCursorPosition(left, top);
            Console.Write(new String(HorizontalLine, numberOfChars));
        }

        // Draw an vertical line
        public void DrawVerticalLine(int left, int top)
        {
            Console.ForegroundColor = borderColor;
            Console.SetCursorPosition(left, top);
            Console.Write(VerticalLine);
        }

        // Draw the text and numbers around the board
        public void DrawText(string text, int left, int top)
        {
            Console.BackgroundColor = textBackColor;
            Console.ForegroundColor = textColor;
            Console.SetCursorPosition(left - 1, top);
            Console.Write(" {0} ", text);
            Console.BackgroundColor = backColor;
        }

        // Draw unplaced player pieces before moving
        public void DrawPlayerUnplacedPieces(Player player)
        {
            int allPieces = 9;
            int numberOfPieceForPlacing = player.piece.Count - player.piece.GetPiecesPlacedOnBoard();
            int col = player.Display.x;
            int row = player.Display.y;

            for (int i = 0; i < allPieces; i++)
            {
                Console.ResetColor();
                if (i % 3 == 0)
                {
                    row += 2;
                    col = player.Display.x;
                }
                if (i < numberOfPieceForPlacing)
                {
                    DrawPiecePlace(col, row, player.piece.PiecesColor, "███");
                }
                else
                {
                    DrawPiecePlace(col, row, ConsoleColor.Black, "███");
                }
                col += 4;
            }
        }

        // Draw centered player names before moving
        public void DrawPlayerName(Player player, int width, int lenght)
        {
            int placeForName = 11;
            int nameLength = player.Name.Length;
            int nameXPosition = (placeForName - nameLength) / 2;

            // Check player 1 or 2 and center his name position
            Console.SetCursorPosition(
               player.Display.x + nameXPosition + ((player.Display.x < width / 2) ? lenght + 1 : -(lenght + 3)),
               player.Display.y + 4);

            Console.ForegroundColor = player.piece.PiecesColor;
            Console.WriteLine(player.Name);
            Console.ResetColor();
        }
    }
}