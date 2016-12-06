namespace BankSystem.Client
{
    using Data;
    using Execution;
    using OI;

    public class Startup
    {
        public static void Main(string[] args)
        {
            BankSystemContext db = new BankSystemContext();
            IConsoleReader reader = new ConsoleReader();
            IConsoleWriter writer = new ConsoleWriter();
            IEngine engine = new Engine(db, reader, writer);
            engine.Run();

        }
    }
}
