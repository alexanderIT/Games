namespace Minesweeper
{
    using System;
    using Contracts;

    public class MinesweeperField : FieldFactory
    {
        private const int FieldRows = 5;
        private const int FieldCols = 10;

        public override IField CreateField()
        {
            IField field = new Field(FieldRows, FieldCols);
            IGenerator mineGenerator = new MineGenerator(field);
            IGeneratable mine = new Mine('*');
            IAdjacencyMap mineMap = new MinesweeperAdjacencyMap(field, mine);

            mineGenerator.Generate(mine, FieldRows + FieldCols);
            mineMap.CreateNeighboursMap();

            return field;
        }
    }
}