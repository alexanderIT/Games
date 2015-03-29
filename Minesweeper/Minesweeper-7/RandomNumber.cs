namespace Minesweeper
{
    using System;

    public static class RandomNumber
    {
        static Random generator = new Random();
        
        public static int Get(int maxNumber)
        {
            int number = generator.Next(maxNumber);
            return number;
        }

        public static int Get(int minNumber, int maxNumber)
        {
            int number = generator.Next(minNumber, maxNumber);
            return number;
        }
    }
}
