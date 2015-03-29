namespace Minesweeper.Contracts
{
    public interface IGenerator
    {
        void Generate(IGeneratable item, int itemCount);
    }
}
