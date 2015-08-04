namespace TheMillGame
{
    using System;

    interface IPlayer
    {
        Piece piece { get; set; }
        Position Display { get; set; }
        ConsoleColor Color { get; set; }
        string Name { get; set; }

        bool Loose(bool[,] op);
    }
}
