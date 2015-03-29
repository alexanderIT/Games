namespace BattleshipGame
{
    using System;
    using BattleshipGame.Contracts;

    public class ReadInput : IReadInput
    {
        /// <summary>
        /// String with coordinates read from player.
        /// </summary>
        public string playerInput { get; set; }
        /// <summary>
        /// X coordinate after parse string with coordinates.
        /// </summary>
        public int xCoord { get; set; }
        /// <summary>
        /// Y coordinate after parse string with coordinates.
        /// </summary>
        public int yCoord { get; set; }
        /// <summary>
        /// Marker for valid input string.
        /// </summary>
        public bool validInput { get; set; }
        /// <summary>
        /// Marker for "show" command occured.
        /// </summary>
        public bool IsCommand { get; set; }
        /// <summary>
        /// Instans of command.
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// Perform the Execute method of specific "show" command.
        /// </summary>
        public void ExecuteShowCommand()
        {
            this.Command.Execute();
        }

        /// <summary>
        /// Print error in console.
        /// </summary>
        /// <param name="error">String with generated error.</param>
        public void PrintError(string error)
        {
            Console.WriteLine(error);
        }

        private int ConvertLetterToInt(string letter)
        {
            char convertesLetter;
            int ycordinate = 0;
            convertesLetter = Char.Parse(letter.ToUpper());
            return ycordinate = convertesLetter - 65;
        }

        /// <summary>
        /// Read, parse and validate input string get from player.
        /// </summary>
        public void ReadAndValidateInput()
        {
            string error;
            this.IsCommand = false;

            do
            {
                Console.Write("Enter coordinates (row, col), e.g. A5 =");

                playerInput = Console.ReadLine();

                playerInput = playerInput.Trim(); // Removies any spaces on either end

                if (playerInput != "show")
                {
                    string yCoordString = playerInput.Substring(0, 1);
                    yCoordString = yCoordString.Trim(); // Removes any spaces on either end
                    yCoord = this.ConvertLetterToInt(yCoordString);

                    string xCoordString = playerInput.Substring(1);
                    xCoordString = xCoordString.Trim(); // Removes any spaces on either end
                    xCoord = int.Parse(xCoordString);

                    if ((xCoord >= 0) && (xCoord < 10) && (yCoord >= 0) && (yCoord < 10))
                    {
                        this.validInput = true; // Set to "true" so loop can exit
                    }
                    else if ((yCoord < 0) || (yCoord > 10))
                    {
                        error = "Your Y value is out of range!";
                        this.validInput = false;
                        PrintError(error);
                    }
                    else if ((xCoord < 0) || (xCoord > 10))
                    {
                        error = "Your X value is too long or too small!";
                        this.validInput = false;
                        PrintError(error);
                    }
                    else
                    {
                        error = "Your cordinate is no valid!";
                        PrintError(error);
                    }

                }
                else
                {
                    this.IsCommand = true;
                    return;
                }

            }
            while (!validInput);
        }
    }
}
