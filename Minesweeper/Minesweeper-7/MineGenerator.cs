namespace Minesweeper
{
    using System;
    using Contracts;

    public class MineGenerator : IGenerator
    {
        private readonly IField field;

        /// <summary>
        /// Given a field the MineGenerator can populate it with any number of IGeneratable items
        /// </summary>
        /// <param name="fieldToPopulate">The field which will be populated</param>
        public MineGenerator(IField fieldToPopulate)
        {
            this.field = fieldToPopulate;
        }

        /// <summary>
        /// Generates an item any given times
        /// </summary>
        /// <param name="item">The item to generate</param>
        /// <param name="itemCount">Times the item will be generated</param>
        public void Generate(IGeneratable item, int itemCount)
        {
            int generatedMines = 0;

            while (generatedMines < itemCount)
            {
                int rowToPutMine = RandomNumber.Get(this.field.Rows);
                int colToPutMine = RandomNumber.Get(this.field.Columns);

                if (field[rowToPutMine, colToPutMine] != item.Symbol)
                {
                    field[rowToPutMine, colToPutMine] = item.Symbol;
                    generatedMines++;
                }
            }
        }
    }
}
