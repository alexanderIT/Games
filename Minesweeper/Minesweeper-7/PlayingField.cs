namespace Minesweeper
{
    using System;
    using Contracts;

    public class PlayingField : FieldFactory
    {
        private const int FieldRows = 5;
        private const int FieldCols = 10;

        public override IField CreateField()
        {
            IField field = new Field(FieldRows, FieldCols);
            IGenerator fieldPopulator = new GeneralFieldPopulator(field);
            IGeneratable cellRepresentor = new CellRepresentor('?');

            fieldPopulator.Generate(cellRepresentor, field.Rows * field.Columns);

            return field;
        }
    }
}
