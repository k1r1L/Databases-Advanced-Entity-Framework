namespace _01.BookshopSystem
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;
    using EntityFramework.Extensions;
    using System.Data.SqlClient;
    using System.Data;
    public class Startup
    {
        public static void Main(string[] args)
        {
            BookshopSystemContext context = new BookshopSystemContext();
            //01. BookTitlesByAgeRegistration(context);
            //02. GoldenBooks(context);
            //03. BooksByPrice(context);
            //04. NotReleasedBooks(context);
            //05. BookTitlesByCategories(context);
            //06. BooksReleasedBeforeDate(context);
            //07. AuthorsSearch(context);
            //08. BooksSearch(context);
            //09. BookTitlesSearch(context);
            //10. CountBooks(context);
            //11. TotalBookCopies(context);
            //12. FindProfit(context);
            //13. MostRecentBooks(context);
            //14. IncreaseBookCopies(context);
            //15. RemoveBooks(context);
            //16. StoredProcedure(context);
        }

        private static void StoredProcedure(BookshopSystemContext context)
        {
            Console.Write("Please enter the first and last name of the author separated by a single space: ");
            string[] names = Console.ReadLine().Split(' ');
            SqlParameter firstName = new SqlParameter("@firstName", SqlDbType.NVarChar);
            SqlParameter lastName = new SqlParameter("@lastName", SqlDbType.NVarChar);
            firstName.Value = names[0];
            lastName.Value = names[1];
            int numberOfBooksWritten = context.Database.SqlQuery<int>("[dbo].[usp_GetNumberOfBooksForAuthor] @firstName, @lastName", firstName, lastName).Single();

            if (numberOfBooksWritten != 0)
            {
                Console.WriteLine($"{firstName.Value} {lastName.Value} has written {numberOfBooksWritten} books");
            }
            else
            {
                Console.WriteLine($"{firstName.Value} {lastName.Value} has not written any books yet");
            }
        }

        private static void RemoveBooks(BookshopSystemContext context)
        {
            int copiesAmount = int.Parse(Console.ReadLine());
            int numOfBooksDeleted = context.Books
                .Where(b => b.Copies < copiesAmount)
                .Delete();

            Console.WriteLine($"{numOfBooksDeleted} books were deleted");
        }

        private static void IncreaseBookCopies(BookshopSystemContext context)
        {
            DateTime dateGiven = DateTime.ParseExact(
                Console.ReadLine(), "dd MMM yyyy", CultureInfo.InvariantCulture);
            int copiesPerBook = int.Parse(Console.ReadLine());

            int numOfBooks = context.Books
                .Where(b => b.ReleaseDate > dateGiven)
                .Update(
                b => new Book() { Copies = b.Copies + copiesPerBook });

            int amount = copiesPerBook * numOfBooks;
            Console.WriteLine($"{numOfBooks} books are released after {dateGiven.ToString("dd MMM yyyy")} so total of {amount} book copies were added");
        }

        private static void MostRecentBooks(BookshopSystemContext context)
        {
            var categoryInfos = context.Categories
                .Where(c => c.Books.Count > 35)
                .Select(category => new
                {
                    CategoryName = category.Name,
                    TotalBooksCount = category.Books.Count,
                    Top3MostRecentBooks = category.Books
                    .OrderByDescending(b => b.ReleaseDate)
                    .ThenBy(b => b.Title)
                    .Select(b => new
                    {
                        BookTitle = b.Title,
                        ReleaseYear = b.ReleaseDate.Value.Year
                    })
                    .Take(3)
                })
                .OrderByDescending(c => c.TotalBooksCount);

            foreach (var categoryInfo in categoryInfos)
            {
                Console.WriteLine($"--{categoryInfo.CategoryName}: {categoryInfo.TotalBooksCount} books");
                foreach (var book in categoryInfo.Top3MostRecentBooks)
                {
                    Console.WriteLine($"{book.BookTitle} ({book.ReleaseYear})");
                }
            }

        }

        private static void FindProfit(BookshopSystemContext context)
        {
            var categories = context.Categories
                .GroupBy(category => new
                {
                    Name = category.Name,
                    Profit = category.Books.Sum(b => b.Price * b.Copies)
                })
                .OrderByDescending(c => c.Key.Profit)
                .ThenBy(c => c.Key.Name);

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Key.Name} - ${category.Key.Profit}");
            }
        }

        private static void TotalBookCopies(BookshopSystemContext context)
        {
            var authors = context.Authors
                .GroupBy(author => new
                {
                    FullName = author.FirstName + " " + author.LastName,
                    Copies = author.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.Key.Copies);

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.Key.FullName} - {author.Key.Copies}");
            }
        }

        private static void CountBooks(BookshopSystemContext context)
        {
            int titleLength = int.Parse(Console.ReadLine());
            int numOfBooks = context.Books.Where(b => b.Title.Length > titleLength).Count();
            Console.WriteLine($"There are {numOfBooks} books with longer title than {titleLength} symbols");
        }

        private static void BookTitlesSearch(BookshopSystemContext context)
        {
                string str = Console.ReadLine();
                context.Books
                    .Where(b => b.Author.LastName.StartsWith(str))
                    .Select(b => b.Title)
                    .ToList()
                    .ForEach(Console.WriteLine);
        }

        private static void BooksSearch(BookshopSystemContext context)
        {
                string containingStr = Console.ReadLine();
                context.Books
                    .Where(b => b.Title.Contains(containingStr))
                    .Select(b => b.Title)
                    .ToList()
                    .ForEach(Console.WriteLine);
        }

        private static void AuthorsSearch(BookshopSystemContext context)
        {
                string endStr = Console.ReadLine();

                var authors = context.Authors
                    .Where(a => a.FirstName.EndsWith(endStr))
                    .Select(a => new
                    {
                        a.FirstName,
                        a.LastName
                    });

                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.FirstName} {author.LastName}");
                }
        }

        private static void BooksReleasedBeforeDate(BookshopSystemContext context)
        {
            DateTime releaseDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var books = context.Books
                .Where(b => b.ReleaseDate < releaseDate)
                .Select(b => new
                {
                    b.Title,
                    EditionType = (EditionType)b.EditionType,
                    b.Price
                });

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - {book.EditionType} - {book.Price}");
            }
        }

        private static void BookTitlesByCategories(BookshopSystemContext context)
        {
            List<string> categories = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();


            var categoryEntities = context.Categories
                .Where(c => categories.Contains(c.Name))
                .Select(c => new
                {
                    BookTitles = c.Books.Select(b => b.Title)
                });

            foreach (var book in categoryEntities)
            {
                Console.WriteLine(string.Join("\n", book.BookTitles));
            }

        }

        private static void NotReleasedBooks(BookshopSystemContext context)
        {
            int year = int.Parse(Console.ReadLine());

            string[] bookTitles = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => b.Title)
                .ToArray();

            foreach (string bookTitle in bookTitles)
            {
                Console.WriteLine(bookTitle);
            }

        }

        private static void BooksByPrice(BookshopSystemContext context)
        {
            var booksByPrice = context.Books
                .Where(b => b.Price < 5 || b.Price > 40)
                .Select(b => new
                {
                    Title = b.Title,
                    Price = b.Price
                });

            foreach (var book in booksByPrice)
            {
                Console.WriteLine($"{book.Title} - ${book.Price:F2}");
            }
        }

        private static void GoldenBooks(BookshopSystemContext context)
        {

            List<string> goldenEditionBooks = context.Books
                .Where(b => b.EditionType == 2 && b.Copies < 5000)
                .Select(b => b.Title)
                .ToList();

            goldenEditionBooks.ForEach(Console.WriteLine);

        }

        private static void BookTitlesByAgeRegistration(BookshopSystemContext context)
        {

            string ageRestrictionAsString = Console.ReadLine().ToUpper();
            int ageRestrictionAsInt = 0;
            switch (ageRestrictionAsString)
            {
                case "MINOR":
                    ageRestrictionAsInt = 0;
                    break;
                case "TEEN":
                    ageRestrictionAsInt = 1;
                    break;
                case "ADULT":
                    ageRestrictionAsInt = 2;
                    break;
                default:
                    Console.WriteLine("Invalid age restriction!");
                    return;
            }

            List<string> books = context.Books
                .Where(b => b.AgeRestriction == ageRestrictionAsInt)
                .Select(b => b.Title)
                .ToList();

            books.ForEach(Console.WriteLine);

        }
    }
}
