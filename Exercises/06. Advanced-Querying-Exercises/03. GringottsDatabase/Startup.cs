namespace _03.GringottsDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main(string[] args)
        {
            GringottsContext context = new GringottsContext();
            //19. DeopsitSumOfOllivanderFamily(context);
            //20. DepositsFilter(context);
        }

        private static void DepositsFilter(GringottsContext context)
        {
            context.WizzardDeposits
                .Where(wd => wd.MagicWandCreator == "Ollivander family")
                .GroupBy(wizzardDeposit => wizzardDeposit.DepositGroup)
                .Select(wizzardDeposit => new
                {
                    DepositGroup = wizzardDeposit.Key,
                    TotalDepositAmount = wizzardDeposit.Sum(wd => wd.DepositAmount)
                })
                .Where(wd => wd.TotalDepositAmount < 150000)
                .OrderByDescending(wd => wd.TotalDepositAmount)
                .ToList()
                .ForEach(wd => Console.WriteLine($"{wd.DepositGroup} - {wd.TotalDepositAmount:F2}"));
        }

        private static void DeopsitSumOfOllivanderFamily(GringottsContext context)
        {
            context.WizzardDeposits
                .Where(wd => wd.MagicWandCreator == "Ollivander family")
                .GroupBy(wizzardDeposit => wizzardDeposit.DepositGroup)
                .Select(wizzardDeposit => new
                {
                    DepositGroup = wizzardDeposit.Key,
                    TotalDepositAmount = wizzardDeposit.Sum(wd => wd.DepositAmount)
                })
                .ToList()
                .ForEach(wd => Console.WriteLine($"{wd.DepositGroup} - {wd.TotalDepositAmount:F2}"));
        }
    }
}
