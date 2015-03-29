namespace Minesweeper.Contracts
{
    using System;
    /// <summary>
    /// Represents the engine that executes the main logic of the game. 
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Controls the gameplay throughout other methods for validation and I/O.
        /// </summary>
        void Play();
    }
}
