namespace Minesweeper
{
    using System;
    using System.Linq;
    using Contracts;

    /// <summary>
    /// Represents the engine that executes the main logic of the game. 
    /// </summary>
    public class Engine: IEngine
    {
        private readonly IRenderer renderer;
        private readonly IReadInput inputReader;
        private readonly IScoreBoard scoreBoard;
        private IField mineField;
        private IField playingField;

        private bool playing;

        private string currentUserName;
        private string currentUserInput;

        private int currentUserScore;
        private int openedCells;

        /// <summary>
        /// Represents Engine class constructor.
        /// </summary>
        /// <param name="mineField">Must contain a class that inherit IField interface</param>
        /// <param name="playingField">Must contain a class that inherit IField interface</param>
        /// <param name="renderer">Must contain a class that inherit IRenderer interface</param>
        /// <param name="inputReader">Must contain a class that inherit IReadInput interface</param>
        public Engine(IField mineField, IField playingField, IRenderer renderer, IReadInput inputReader)
        {
            this.renderer = renderer;
            this.inputReader = inputReader;
            this.scoreBoard = new ScoreBoard();

            this.playingField = playingField;
            this.mineField = mineField;

            this.playing = true;
            this.currentUserName = "Player";
            this.currentUserScore = 0;
            this.openedCells = 0;
        }

        /// <summary>
        /// This method represents the main loop of the game.
        /// </summary>
        public void Play()
        {
            this.renderer.RenderInitialMessage();
            this.renderer.RenderGameField(this.playingField, false);

            while (playing)
            {
                currentUserInput = this.inputReader.GetUserInput();
                CheckCurrentCommand(currentUserInput);
            }
            
        }

        /// <summary>
        /// This method gets the user input and execute its value
        /// </summary>
        /// <param name="command">User input command as string</param>
        private void CheckCurrentCommand(string command)
        {
            string clearedCommand = command.Replace(" ", String.Empty);

            if (clearedCommand.Length == 2)
            {
                int currentRow = int.Parse(clearedCommand[0].ToString());
                int currentCol = int.Parse(clearedCommand[1].ToString());

                if (this.mineField[currentRow, currentCol] == '*')
                {
                    EndGame(currentRow, currentCol);
                }
                else
                {
                    OpenNewCell(currentRow, currentCol);
                }           
            }
            else
            {
                switch (clearedCommand)
                {
                    case "exit": 
                        this.renderer.RenderQuitMessage();
                        Environment.Exit(1); break;
                    case "restart": 
                        RestartGame(); break;
                    case "top":
                        this.renderer.RenderScoreBoard(this.scoreBoard); break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// When the player steps into mine this method ends the game.
        /// </summary>
        /// <param name="row">This must contain current row from the user input</param>
        /// <param name="col">This must contain current column from the user input</param>
        private void EndGame(int row, int col)
        {
            this.playingField[row, col] = this.mineField[row, col];
            this.renderer.RenderGameField(this.playingField, true);
            this.playing = false;
            this.renderer.RenderExplosionMessage(this.openedCells);
            this.currentUserName = this.inputReader.GetUserNickname();
            this.scoreBoard.AddPlayer(this.currentUserName, this.currentUserScore);
            this.renderer.RenderCommandsMessage();
            currentUserInput = this.inputReader.GetUserInput();
            CheckCurrentCommand(currentUserInput);
        }

        /// <summary>
        /// When the player steps on cell that is not mine, engine calls this method.
        /// </summary>
        /// <param name="row">This must contain current row from the user input</param>
        /// <param name="col">This must contain current column from the user input</param>
        private void OpenNewCell(int row, int col)
        {
            this.playingField[row, col] = this.mineField[row, col];
            currentUserScore += int.Parse(this.mineField[row, col].ToString());
            openedCells++;
            this.renderer.RenderGameField(this.playingField, false);
        }

        /// <summary>
        /// When the user enter the "restart" command, this method should be called.
        /// </summary>
        private void RestartGame()
        {
            FieldFactory istanceOfMineFieldFactory = new MinesweeperField();
            this.mineField = istanceOfMineFieldFactory.CreateField();
            FieldFactory instanceOfPlayingFieldFactory = new PlayingField();
            this.playingField = instanceOfPlayingFieldFactory.CreateField();
            this.playing = true;
            Play();
        }
    }
}

