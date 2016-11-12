namespace _04.SalesDatabase
{
    public class Startup
    {
        public static void Main()
        {
            SalesContext context = new SalesContext();
            context.Database.Initialize(true);

            context.SaveChanges();
        }
    }
}
