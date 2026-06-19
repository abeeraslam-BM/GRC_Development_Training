namespace Day1Task1.Models;

// Many-to-many join entity for Book <-> Tag.
public class BookTag
{
    public int BookId { get; set; }
    public int TagId { get; set; }

    public Book? Book { get; set; }
    public Tag? Tag { get; set; }
}