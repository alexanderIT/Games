namespace Minesweeper
{
    using System;
    using Contracts;

    public class GeneralFieldPopulator : IGenerator
    {
        private readonly IField field;

        /// <summary>
        /// Given a field the GeneralFieldPopulator populates the entire field with IGeneratable items
        /// </summary>
        /// <param name="fieldToPopulate">The field which will be populated</param>
        public GeneralFieldPopulator(IField fieldToPopulate)
        {
            this.field = fieldToPopulate;
        }

        public void Generate(IGeneratable item, int itemCount)
        {
            bool reachedItemCount = false;
            int currentCount = 0;

            for (int row = 0; row < this.field.Rows; row++)
            {
                for (int cols = 0; cols < this.field.Columns; cols++)
                {
                    this.field[row, cols] = item.Symbol;
                    currentCount++;

                    if (currentCount == itemCount)
                    {
                        reachedItemCount = true;
                        break;
                    }
                }

                if (reachedItemCount)
                {
                    break;
                }
            }
        }
    }
}
