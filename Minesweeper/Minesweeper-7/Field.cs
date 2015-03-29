namespace Minesweeper
{
    using System;
    using System.Text;
    using Contracts;

    public class Field : IField
    {
        private char[,] field {get;set;}

        /// <summary>
        /// Create a minefield with a specific size.
        /// </summary>
        /// <param name="rows">The row count of the minefield</param>
        /// <param name="columns">The column count of the minefield</param>
        public Field(int rows, int columns)
        {
            ValidateAndInitializeField(rows, columns);
        }
               

        

        /// <summary>
        /// Get the row count of the minefield
        /// </summary>
        public int Rows
        {
            get { return this.field.GetLength(0); }
        }

        /// <summary>
        /// Get the column count of the minefield
        /// </summary>
        public int Columns
        {
            get { return this.field.GetLength(1); }
        }

        /// <summary>
        /// Gets or sets the character in the specified cell
        /// </summary>
        /// <param name="row">The number of the rows</param>
        /// <param name="col">The number of the columns</param>
        /// <returns>The character that is in the specified cell</returns>

        public char this[int row, int col]
        {
            get
            {
                ValidateIndexes(row, col);

                return this.field[row, col];
            }

            set
            {
                ValidateIndexes(row, col);

                this.field[row, col] = value;
            }
        }

        public override string ToString()
        {

            StringBuilder stringifyField = new StringBuilder();

            Console.WriteLine();
            Console.WriteLine();
            
            stringifyField.AppendLine("    0 1 2 3 4 5 6 7 8 9");
            stringifyField.AppendLine("   ----------------------");

            for (int row = 0; row < this.Rows; row++)
            {
                stringifyField.Append(row + " | ");
                for (int col = 0; col < this.Columns; col++)
                {
                    stringifyField.Append(this.field[row, col] + " ");
                }
                stringifyField.AppendLine("|");
            }

            stringifyField.AppendLine("   ----------------------");

            return stringifyField.ToString();
        }

        private void ValidateIndexes(int row, int col)
        {
            if (row < 0 || row >= this.Rows)
            {
                Console.WriteLine("No such row ");
            }

            if (col < 0 || col >= this.Columns)
            {
                Console.WriteLine("No such column.");
            }
            
        }
       

        private void ValidateAndInitializeField(int rows, int cols)
        {
            if (rows < 5)
            {
                throw new ArgumentException("The rows must be at least 5.");
            }

            if (cols < 5)
            {
                throw new ArgumentException("The columns must be at least 5.");
            }

            this.field = new char[rows, cols];
        }

        // These methods should be in separate classes.
        // May use some creational pattern to build
        // the field and populate it with mines.
        #region Methods that need to be extracted
        /// <summary>
        /// Generate mines on the current field and fill the number
        /// of neighbouring mines.
        /// </summary>
        public void GenerateMinefield()
        {
            this.GenerateMines();
            this.GenerateNeighbouringMinesCount();
        }

        private void GenerateMines()
        {
            int rowCount = this.field.GetLength(0);
            int colCount = this.field.GetLength(1);

            int minesToGenerateCount = rowCount + colCount;

            Random rnd = new Random();

            while (minesToGenerateCount > 0)
            {
                int rowToMine = rnd.Next(rowCount);
                int colToMine = rnd.Next(colCount);

                if (this.field[rowToMine, colToMine] != '*')
                {
                    this.field[rowToMine, colToMine] = '*';
                    minesToGenerateCount--;
                }
            }
        }

        private void GenerateNeighbouringMinesCount()
        {
            for (int row = 0; row < this.field.GetLength(0); row++)
            {
                for (int col = 0; col < this.field.GetLength(1); col++)
                {
                    if (this.field[row, col] == '*')
                    {
                        continue;
                    }

                    string neighbourMinesCountAsString = this.CalculateNeighbouringMinesCount(row, col).ToString();
                    this.field[row, col] = Convert.ToChar(neighbourMinesCountAsString);
                }
            }
        }

        private int CalculateNeighbouringMinesCount(int row, int col)
        {
            int[] rowPositions = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] colPositions = { -1, 0, 1, 1, 1, 0, -1, -1 };

            int neighbouringMinesCount = 0;
            int currentNeighbourRow = 0;
            int currentNeighbourCol = 0;

            for (int position = 0; position < 8; position++)
            {
                currentNeighbourRow = row + rowPositions[position];
                currentNeighbourCol = col + colPositions[position];

                if (currentNeighbourRow < 0 || currentNeighbourRow >= this.field.GetLength(0) ||
                    currentNeighbourCol < 0 || currentNeighbourCol >= this.field.GetLength(1))
                {
                    continue;
                }

                if (this.field[currentNeighbourRow, currentNeighbourCol] == '*')
                {
                    neighbouringMinesCount++;
                }
            }

            return neighbouringMinesCount;
        }
        #endregion
    }
}
