namespace BankSystem.Client.OI
{
    using System;

    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string inputLine)
        {
            Console.WriteLine(inputLine);
        }
    }
}
