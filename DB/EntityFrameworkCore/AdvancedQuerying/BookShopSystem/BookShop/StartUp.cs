namespace BookShop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BookShop.Models;
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

                string result = GetMostRecentBooks(db);

                Console.WriteLine(result);
            }
        }

        // Problem 1. Age Restriction
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

        // Problem 5. Book Titles by Category
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

        // Problem 9. Book Search by Author
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

        // Problem 11. Total Book Copies
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

        // Problem 13. Most Recent Books
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
    }
}
