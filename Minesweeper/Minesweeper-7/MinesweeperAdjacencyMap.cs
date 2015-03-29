namespace Minesweeper
{
    using System;
    using Contracts;

    /// <summary>
    /// Creates the adjacency map fot he minesweeper field
    /// </summary>
    public class MinesweeperAdjacencyMap : IAdjacencyMap
    {
        private readonly IField field;
        private readonly IGeneratable item;

        /// <summary>
        /// Populates the IField with the count of IGeneratables next to each empty cell
        /// </summary>
        /// <param name="field">The field which will be mapped</param>
        /// <param name="item">The item which the mapping will be calculated for</param>
        public MinesweeperAdjacencyMap(IField field, IGeneratable item)
        {
            this.field = field;
            this.item = item;
        }
        
        /// <summary>
        /// Map each empty cell of the field with its neighbouring items
        /// </summary>
        public  void CreateNeighboursMap()
        {
            for (int row = 0; row < this.field.Rows; row++)
            {
                for (int col = 0; col < this.field.Columns; col++)
                {
                    if (this.field[row, col] == this.item.Symbol)
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
            int numberOfPosition = rowPositions.Length;

            int neighbouringMinesCount = 0;
            int currentNeighbourRow = 0;
            int currentNeighbourCol = 0;

            for (int position = 0; position < numberOfPosition; position++)
            {
                currentNeighbourRow = row + rowPositions[position];
                currentNeighbourCol = col + colPositions[position];

                if (currentNeighbourRow < 0 || this.field.Rows <= currentNeighbourRow  ||
                    currentNeighbourCol < 0 || this.field.Columns <= currentNeighbourCol)
                {
                    continue;
                }

                if (this.field[currentNeighbourRow, currentNeighbourCol] == this.item.Symbol)
                {
                    neighbouringMinesCount++;
                }
            }

            return neighbouringMinesCount;
        }
    }
}
