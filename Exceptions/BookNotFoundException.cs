namespace Day1Task1.Exceptions;

public class BookNotFoundException : Exception
{
    public BookNotFoundException(int id): base($"Book with Id {id} was not found.")
    {
    }
}