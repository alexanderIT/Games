namespace Minesweeper.Contracts
{
    public interface IField
    {
        int Rows { get; }

        int Columns { get; }

        char this[int row, int col] { get; set; }

        string ToString();
    }
}
