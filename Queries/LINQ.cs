using Day1Task1.Data;
using Day1Task1.Models;

namespace Day1Task1.Queries;

public static class Linq
{
    public static void RunAll()
    {
        Query1_FilterByAuthor();
        Query2_TitlesSortedAlphabetically();
        Query3_GroupByAuthor();
        Query4_AveragePages();
        Query5_AnyOver500Pages();
        Query6_FirstOrDefaultById();
        Query7_JoinBooksAndAuthors();
        Query8_Top3Longest();
    }

    public static void Query1_FilterByAuthor()
    {
        Console.WriteLine("\nQuery 1");

        var filteredBooks = InMemoryStore.Books
            .Where(b => b.AuthorId == 1)
            .ToList();

        foreach (var b in filteredBooks)
            Console.WriteLine($"  {b.Title} ({b.Year})");
    }

    public static void Query2_TitlesSortedAlphabetically()
    {
        Console.WriteLine("\nQuery 2");

        var titles = InMemoryStore.Books
            .Select(b => b.Title)
            .OrderBy(t => t)
            .ToList();

        foreach (var t in titles)
            Console.WriteLine($"  {t}");
    }

    public static void Query3_GroupByAuthor()
    {
        Console.WriteLine("\nQuery 3");

        var groups = InMemoryStore.Books
            .GroupBy(b => b.AuthorId)
            .Select(g => new
            {
                AuthorName = InMemoryStore.Authors.First(a => a.Id == g.Key).Name,
                Count = g.Count(),
                Titles = g.Select(b => b.Title)
            });

        foreach (var g in groups)
        {
            Console.WriteLine($"  {g.AuthorName} ({g.Count} books):");
            foreach (var title in g.Titles)
                Console.WriteLine($"    - {title}");
        }
    }

    public static void Query4_AveragePages()
    {
        Console.WriteLine("\nQuery 4");

        double avg = InMemoryStore.Books.Average(b => b.PageCount);
        Console.WriteLine($"  Average pages: {avg:F1}");
    }

    public static void Query5_AnyOver500Pages()
    {
        Console.WriteLine("\nQuery 5");

        bool anyOver500 = InMemoryStore.Books.Any(b => b.PageCount > 500);
        Console.WriteLine($"  Result: {anyOver500}");
    }

    public static void Query6_FirstOrDefaultById()
    {
        Console.WriteLine("\nQuery 6");

        Book? book = InMemoryStore.Books.FirstOrDefault(b => b.Id == 5);
        Console.WriteLine(book is null ? "  Not found" : $"  Found: {book.Title}");

        Console.WriteLine("--- Query 6b: FirstOrDefault by Id = 999 (missing) ---");
        Book? missing = InMemoryStore.Books.FirstOrDefault(b => b.Id == 999);
        Console.WriteLine(missing is null ? "  Not found (null)" : missing.Title);
    }

    public static void Query7_JoinBooksAndAuthors()
    {        
        Console.WriteLine("\nQuery 7");
        var joined = from b in InMemoryStore.Books
                     join a in InMemoryStore.Authors on b.AuthorId equals a.Id
                     select new { b.Title, AuthorName = a.Name, b.Year };

        foreach (var row in joined)
            Console.WriteLine($"  \"{row.Title}\" by {row.AuthorName} ({row.Year})");
    }

    public static void Query8_Top3Longest()
    {
        Console.WriteLine("\nQuery 8");

        var top3 = InMemoryStore.Books
            .OrderByDescending(b => b.PageCount)
            .Take(3)
            .Select(b => new { b.Title, b.PageCount });

        foreach (var b in top3)
            Console.WriteLine($"  {b.Title}: {b.PageCount} pages");
    }
}