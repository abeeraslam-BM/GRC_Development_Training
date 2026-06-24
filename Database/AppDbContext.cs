using Day1Task1.Models;
using Microsoft.EntityFrameworkCore;

namespace Day1Task1.Database;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<BookTag> BookTags { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookTag>()
            .HasKey(bt => new { bt.BookId, bt.TagId });

        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "J.R.R. Tolkien" },
            new Author { Id = 2, Name = "Isaac Asimov" },
            new Author { Id = 3, Name = "Agatha Christie" }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "The Hobbit", Year = 1937, PageCount = 310, AuthorId = 1 },
            new Book { Id = 2, Title = "The Fellowship of the Ring", Year = 1954, PageCount = 423, AuthorId = 1 },
            new Book { Id = 3, Title = "The Two Towers", Year = 1954, PageCount = 352, AuthorId = 1 },
            new Book { Id = 4, Title = "Foundation", Year = 1951, PageCount = 255, AuthorId = 2 },
            new Book { Id = 5, Title = "I, Robot", Year = 1950, PageCount = 224, AuthorId = 2 },
            new Book { Id = 6, Title = "The Caves of Steel", Year = 1954, PageCount = 206, AuthorId = 2 },
            new Book { Id = 7, Title = "Murder on the Orient Express", Year = 1934, PageCount = 256, AuthorId = 3 },
            new Book { Id = 8, Title = "And Then There Were None", Year = 1939, PageCount = 272, AuthorId = 3 }
        );

        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "Fantasy" },
            new Tag { Id = 2, Name = "Science Fiction" },
            new Tag { Id = 3, Name = "Mystery" },
            new Tag { Id = 4, Name = "Classic" }
        );

        modelBuilder.Entity<BookTag>().HasData(
            new BookTag { BookId = 1, TagId = 1 },
            new BookTag { BookId = 2, TagId = 1 },
            new BookTag { BookId = 1, TagId = 4 },
            new BookTag { BookId = 4, TagId = 2 },
            new BookTag { BookId = 5, TagId = 2 },
            new BookTag { BookId = 7, TagId = 3 }
        );
    }
}