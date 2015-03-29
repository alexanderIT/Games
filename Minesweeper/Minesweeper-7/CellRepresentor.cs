namespace Minesweeper
{
    using System;
    using Contracts;

    public class CellRepresentor : IGeneratable
    {
        private char symbol;

        /// <summary>
        /// Creates a mine with a given symbol
        /// </summary>
        /// <param name="symbol">The symbol to represent the mine</param>
        public CellRepresentor(char symbol)
        {
            this.Symbol = symbol;
        }

        /// <summary>
        /// Gets the symbol that represents this mine instance
        /// </summary>
        public char Symbol
        {
            get { return this.symbol; }
            private set { this.symbol = value; }
        }
    }
}
