namespace TestMinesweeper
{
    using System;
    using System.IO;

    internal class ConsoleRedirector : IDisposable
    {
        private StringWriter consoleOutput = new StringWriter();
        private TextWriter originalConsoleOutput;

        public ConsoleRedirector()
        {
            this.originalConsoleOutput = Console.Out;
            Console.SetOut(consoleOutput);
        }
        public void Dispose()
        {
            Console.SetOut(originalConsoleOutput);
            Console.Write(this.ToString());
            this.consoleOutput.Dispose();
        }
        public override string ToString()
        {
            return this.consoleOutput.ToString();
        }
    }
}
