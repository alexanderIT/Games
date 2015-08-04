namespace TheMillGame
{
    using System;

    public class RealPlayer : Player
    {
        private int winsCount;

        public int WinsCount
        {
            set
            {
                if (value < 0)
                {
                    winsCount = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The count of the wins can't be negative!");
                }
            }

            get
            {
                return winsCount;
            }
        }

        // Constructor
        public RealPlayer(Piece playersPieces, string playersName = "Player", int wins = 0)
            : base(playersPieces, playersName)
        {
            this.winsCount = wins;
        }
    }
}