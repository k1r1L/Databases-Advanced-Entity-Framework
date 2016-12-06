namespace _01.AdsDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AdsDatabaseStartup
    {
        public static void Main(string[] args)
        {
            AdsContext context = new AdsContext();
            context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS");

            // Task 01
            //WithoutInclude(context);
            //WithInclude(context);

            // Task 02
            //PlayWithToList(context);

            // Task 03
            //SelectAllColumns(context);
            //SelectOneColumn(context);


        }

        private static void SelectOneColumn(AdsContext context)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            IEnumerable<string> adsTitle = context.Ads.Select(ad => ad.Title);

            foreach (string adTitle in adsTitle)
            {
                Console.WriteLine(adTitle);
            }

            timer.Stop();
            Console.WriteLine($"Select one column elapsed time: {timer.Elapsed}");
        }

        private static void SelectAllColumns(AdsContext context)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            IEnumerable<Ad> ads = context.Ads;

            foreach (Ad ad in ads)
            {
                Console.WriteLine(ad.Title);
            }

            timer.Stop();
            Console.WriteLine($"Select all columns elapsed time: {timer.Elapsed}");
        }

        private static void PlayWithToList(AdsContext context)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var ads = context.Ads
                .ToList()
                .Where(a => a.AdStatus.Status == "Published")
                .Select(ad => new
                {
                    ad.Title,
                    ad.Category,
                    ad.Town,
                    ad.Date
                })
                .ToList()
                .OrderBy(a => a.Date);
            timer.Stop();

            Console.WriteLine($"Non-optimized ToList(): {timer.Elapsed}");
            timer.Reset();

            timer.Start();
           var optimizedAds = context.Ads
               .Where(a => a.AdStatus.Status == "Published")
               .OrderBy(a => a.Date)
               .Select(ad => new
               {
                   ad.Title,
                   ad.Category,
                   ad.Town,
                   ad.Date
               });

            timer.Stop();
            Console.WriteLine($"Optimized ToList(): {timer.Elapsed}");
        }

        private static void WithInclude(AdsContext context)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            IEnumerable<Ad> ads = context.Ads.Include("AdStatus").Include("Category").Include("Town").Include("AspNetUser");

            foreach (Ad ad in ads)
            {
                Console.WriteLine($"{ad.Title} {ad.AdStatus?.Status} {ad.Category?.Name} {ad.Town?.Name} {ad.AspNetUser?.Name}");
            }

            timer.Stop();
            Console.WriteLine($"Execution with include: {timer.Elapsed}");
        }

        private static void WithoutInclude(AdsContext context)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            IEnumerable<Ad> ads = context.Ads;

            foreach (Ad ad in ads)
            {
                Console.WriteLine($"{ad.Title} {ad.AdStatus?.Status} {ad.Category?.Name} {ad.Town?.Name} {ad.AspNetUser?.Name}");
            }

            timer.Stop();
            Console.WriteLine($"Execution without include: {timer.Elapsed}");
        }
    }
}
