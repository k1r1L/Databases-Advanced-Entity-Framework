namespace _02.Gringotts
{
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            //Problem 19.
            GringottsContext context = new GringottsContext();
            string[] wizards = context.WizzardDeposits
                .Where(wd => wd.DepositGroup == "Troll Chest")
                .Select(wd => wd.FirstName.Substring(0, 1))
                .Distinct()
                .ToArray();

            foreach (var w in wizards)
            {
                Console.WriteLine(w);
            }
        }
    }
}
