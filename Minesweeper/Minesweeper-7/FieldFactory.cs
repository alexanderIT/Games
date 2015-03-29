namespace Minesweeper
{
    using System;
    using Contracts;

    public abstract class FieldFactory
    {
        public abstract IField CreateField();
    }
}
