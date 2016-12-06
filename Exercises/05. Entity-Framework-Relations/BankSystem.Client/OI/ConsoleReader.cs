namespace BankSystem.Client.OI
{
    using System;

    public class ConsoleReader : IConsoleReader
    {
        public string ReadLine()
        {
            string inputLine = Console.ReadLine();

            return inputLine;
        }
    }
}
