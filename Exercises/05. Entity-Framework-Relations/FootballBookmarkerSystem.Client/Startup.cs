namespace FootballBookmarkerSystem.Client
{
    using FootballBookmarkerSystem.Data;

    public class Startup
    {
        public static void Main(string[] args)
        {
            FootballBookmarkerContext db = new FootballBookmarkerContext();

            db.Database.Initialize(true);
            db.SaveChanges();
        }
    }
}
