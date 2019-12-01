namespace BookShop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);

                string command = Console.ReadLine();

                string result = GetGoldenBooks(db);

                Console.WriteLine(result);
            }
        }

        // Problem 1. Age Restriction
        // Return in a single string all book titles, each on a new line, that have age restriction, 
        // equal to the given command. Order the titles alphabetically.
        // Read input from the console in your main method, 
        // and call your method with the necessary arguments.Print the returned string to the console. 
        // Ignore casing of the input.
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b => b.Title)
                .ToList();

            foreach (var b in books)
            {
                sb.AppendLine(b.Title);
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 2. Golden Books
        // Return in a single string titles of the golden edition books that have less than 5000 copies, 
        // each on a new line.Order them by book id ascending.
        // Call the GetGoldenBooks(BookShopContext context) method in your Main() 
        // and print the returned string to the console.
        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .Select(b => new
                {
                    b.EditionType,
                    b.Copies,
                    b.BookId,
                    b.Title
                })
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 3. Books by Price
        // Return in a single string all titles and prices of books with price higher than 40, 
        // each on a new row in the format given below. 
        // Order them by price descending.
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Price,
                    b.Title
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 4. Not Released In
        // Return in a single string all titles of books that are NOT released on a given year. 
        // Order them by book id ascending.
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    b.ReleaseDate,
                    b.BookId,
                    b.Title
                })
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 5. Book Titles by Category
        // Return in a single string the titles of books by a given list of categories. 
        // The list of categories will be given in a single line separated with one or more spaces. 
        // Ignore casing. Order by title alphabetically.
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();

            var books = context
                .Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            return String.Join(Environment.NewLine, books);
        }

        // Problem 6. Released Before Date
        // Return the title, edition type and price of all books that are released before a given date. 
        // The date will be a string in format dd-MM-yyyy.
        // Return all of the rows in a single string, ordered by release date descending.
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder sb = new StringBuilder();

            DateTime dateTime = DateTime.ParseExact(date, "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

            var books = context.Books
                .Select(b => new
                {
                    b.ReleaseDate,
                    b.Title,
                    b.Price,
                    b.EditionType
                })
                .Where(b => b.ReleaseDate < dateTime)
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 7. Author Search
        // Return the full names of authors, whose first name ends with a given string.
        // Return all names in a single string, each on a new row, ordered alphabetically.
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(a => a.FullName)
                .ToList();

            foreach (var author in authors)
            {
                sb.AppendLine(author.FullName);
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 8. Book Search
        // Return the titles of book, which contain a given string. Ignore casing.
        // Return all titles in a single string, each on a new row, ordered alphabetically.
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Title.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b => b.Title)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 9. Book Search by Author
        // Return all titles of books and their authors’ names for books, 
        // which are written by authors whose last names start with the given string.
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var booksAndAuthors = context
                .Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    Author = $"{b.Author.FirstName} {b.Author.LastName}"
                })
                .ToList();

            foreach (var b in booksAndAuthors)
            {
                sb.AppendLine($"{b.Title} ({b.Author})");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 10. Count Books
        // Return the number of books, which have a title longer than the number given as an input.
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books.Where(b => b.Title.Length > lengthCheck).Count();
        }

        // Problem 11. Total Book Copies
        // Return the total number of book copies for each author. 
        // Order the results descending by total book copies.
        // Return all results in a single string, each on a new line.
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var authors = context
                .Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    BookCopies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.BookCopies)
                .ToList();

            foreach (var a in authors)
            {
                sb.AppendLine($"{a.Name} - {a.BookCopies}");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 12. Profit by Category
        // Return the total profit of all books by category. Profit for a book can be 
        // calculated by multiplying its number of copies by the price per single book. 
        // Order the results by descending by total profit for category and ascending by category name.
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Money = c.CategoryBooks.Sum(i => i.Book.Price * i.Book.Copies)
                })
                .OrderByDescending(b => b.Money)
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var category in categories)
            {
                sb.AppendLine($"{category.Name} ${category.Money:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 13. Most Recent Books
        // Get the most recent books by categories. The categories should be ordered by name alphabetically. 
        // Only take the top 3 most recent books from each category - ordered by release date (descending). 
        // Select and print the category name, and for each book – its title and release year.
        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categoriesAndMostRecentBooks = context
                .Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    CategoryRecentBooks = c.CategoryBooks
                                             .OrderByDescending(cb => cb.Book.ReleaseDate)
                                             .Take(3)
                                             .Select(cb => new
                                             {
                                                 BookTitle = cb.Book.Title,
                                                 BookRelease = cb.Book.ReleaseDate.Value.Year
                                             })
                                             .ToList()
                })
                .OrderBy(c => c.CategoryName)
                .ToList();

            foreach (var c in categoriesAndMostRecentBooks)
            {
                sb.AppendLine($"--{c.CategoryName}");

                foreach (var b in c.CategoryRecentBooks)
                {
                    sb.AppendLine($"{b.BookTitle} ({b.BookRelease})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 14. Increase Prices
        // Increase the prices of all books released before 2010 by 5.
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        // Problem 15. Remove Books
        // Remove all books, which have less than 4200 copies. 
        // Return an int - the number of books that were deleted from the database.
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books.Where(b => b.Copies < 4200);
            int count = books.Count();

            foreach (var book in books)
            {
                context.Books.Remove(book);
            }

            context.SaveChanges();

            return count;
        }
    }
}